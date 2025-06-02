using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using MySql.Data.MySqlClient;
using Sklep.admin;

namespace Sklep.Pages
{
    public partial class OrdersView : Page
    {
        public OrdersView()
        {
            InitializeComponent();
            LoadOrderData();
        }

        private void ApplyFilters()
        {
            DateTime startDate = startDatePicker.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = endDatePicker.SelectedDate ?? DateTime.MaxValue;
            string searchText = searchTextBox.Text.Trim();

            List<ord> filteredOrders = new List<ord>();

            foreach (ord order in ordersDataGrid.ItemsSource)
            {
                bool dateMatch = order.Order_Date >= startDate && order.Order_Date <= endDate;
                bool textMatch = string.IsNullOrEmpty(searchText) || order.Order_Number.ToString().Contains(searchText);

                if (dateMatch && textMatch)
                {
                    filteredOrders.Add(order);
                }
            }

            ordersDataGrid.ItemsSource = filteredOrders;
        }

        private void LoadOrderData()
        {
            try
            {
                using (var connection = new MySqlConnection("server=127.0.0.1;user=root;password=0000;database=shop"))
                {
                    connection.Open();

                    string query = @"
                SELECT 
                    OG.Order_Number,
                    OG.Order_Date,
                    OG.Total_Order_Cost,
                    OG.Order_Status,
                    OG.Payment_Status,
                    C.Full_Name AS Client_Name,
                    P.Product_Name,
                    OD.Quantity
                FROM 
                    Order_General_Info OG
                JOIN 
                    Order_Details OD ON OG.Order_Number = OD.Order_Number
                JOIN 
                    Client C ON OG.Client_ID = C.Client_ID
                JOIN 
                    Product P ON OD.Product_ID = P.Product_ID";

                    MySqlCommand command = new MySqlCommand(query, connection);

                    using (var reader = command.ExecuteReader())
                    {
                        List<ord> orders = new List<ord>();
                        int currentOrderNumber = -1;
                        ord currentOrder = null;
                        while (reader.Read())
                        {
                            int orderNumber = reader.GetInt32("Order_Number");
                            // Check if this is a new order
                            if (orderNumber != currentOrderNumber)
                            {
                                // Create a new ord object for the new order
                                currentOrder = new ord
                                {
                                    Order_Number = orderNumber,
                                    Order_Date = reader.GetDateTime("Order_Date"),
                                    Total_Order_Cost = reader.GetDecimal("Total_Order_Cost"),
                                    Order_Status = reader.GetString("Order_Status"),
                                    Payment_Status = reader.GetString("Payment_Status"),
                                    Client_Name = reader.GetString("Client_Name"),
                                    Product_Name = reader.GetString("Product_Name"),
                                    Quantity = reader.GetInt32("Quantity"),
                                };
                                orders.Add(currentOrder);
                                currentOrderNumber = orderNumber;
                            }
                            else
                            {
                                // Add additional products to the current order
                                currentOrder.Product_Name += ", " + reader.GetString("Product_Name");
                                currentOrder.Quantity += reader.GetInt32("Quantity");
                            }
                        }

                        // Встановлення джерела даних для DataGrid
                        ordersDataGrid.ItemsSource = orders;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка завантаження даних про замовлення: " + ex.Message);
            }
        }
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            // Отримати вибраний об'єкт замовлення з властивості Tag кнопки
            if (sender is Button button && button.Tag is ord selectedOrder)
            {
                // Запитати користувача про підтвердження видалення
                MessageBoxResult result = MessageBox.Show("Ви впевнені, що хочете видалити цей ордер?", "Підтвердження видалення", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    // Виконати видалення ордера
                    try
                    {
                        using (var connection = new MySqlConnection("server=127.0.0.1;user=root;password=0000;database=shop"))
                        {
                            connection.Open();

                            // Початок транзакції
                            using (var transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    // Видалення деталей ордера
                                    string deleteOrderDetailsQuery = "DELETE FROM Order_Details WHERE Order_Number = @OrderNumber";
                                    MySqlCommand deleteOrderDetailsCommand = new MySqlCommand(deleteOrderDetailsQuery, connection);
                                    deleteOrderDetailsCommand.Parameters.AddWithValue("@OrderNumber", selectedOrder.Order_Number);
                                    deleteOrderDetailsCommand.ExecuteNonQuery();

                                    // Видалення основного ордера
                                    string deleteOrderQuery = "DELETE FROM Order_General_Info WHERE Order_Number = @OrderNumber";
                                    MySqlCommand deleteOrderCommand = new MySqlCommand(deleteOrderQuery, connection);
                                    deleteOrderCommand.Parameters.AddWithValue("@OrderNumber", selectedOrder.Order_Number);
                                    deleteOrderCommand.ExecuteNonQuery();

                                    // Завершення транзакції
                                    transaction.Commit();

                                    // Після успішного видалення перезавантажте дані (можливо, за допомогою методу LoadOrderData())
                                    LoadOrderData();
                                }
                                catch (Exception ex)
                                {
                                    // У разі помилки відкатити транзакцію
                                    transaction.Rollback();
                                    MessageBox.Show("Помилка видалення ордера: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Помилка видалення ордера: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ResetFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            startDatePicker.SelectedDate = null;
            endDatePicker.SelectedDate = null;
            searchTextBox.Text = "";

            LoadOrderData();
        }

        private void OrdersListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ordersDataGrid.SelectedItem is ord selectedOrder)
            {
                EditOrder editOrderWindow = new EditOrder(selectedOrder);
                editOrderWindow.ShowDialog();
                LoadOrderData();
            }
        }

        private void SearchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (searchTextBox.Text == "Пошук за номером замовлення")
            {
                searchTextBox.Text = "";
                searchTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void SearchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.Text = "Пошук за номером замовлення";
                searchTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}