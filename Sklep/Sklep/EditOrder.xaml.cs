using Microsoft.Identity.Client.NativeInterop;
using MySql.Data.MySqlClient;
using Sklep.admin;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace Sklep
{
    public partial class EditOrder : Window
    {
        private ord currentOrder;

        public EditOrder(ord currentOrder)
        {
            InitializeComponent();
            this.currentOrder = currentOrder;
            PopulateFields();
        }

        private void PopulateFields()
        {
            DateofOrder.Text = currentOrder.Order_Date.ToString("dd.MM.yyyy");
            TotalCost.Text = currentOrder.Total_Order_Cost.ToString();
            Clientname.Text = currentOrder.Client_Name;
            Amount.Text = currentOrder.Quantity.ToString();
            Sellingprice.Text = currentOrder.Sale_Price.ToString();
        }

        private void btnEditOrd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new MySqlConnection("server=127.0.0.1;user=root;password=0000;database=shop"))
                {
                    connection.Open();

                    string query = @"
    UPDATE Order_General_Info
    SET 
        Order_Date = @Order_Date,
        Total_Order_Cost = @Total_Order_Cost
    WHERE Order_Number = @Order_Number;

    UPDATE Client
    SET 
        Full_Name = @Client_Name
    WHERE Client_ID = (
        SELECT Client_ID FROM Order_General_Info WHERE Order_Number = @Order_Number
    );
";


                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Order_Number", currentOrder.Order_Number);

                    // Parsing date
                    DateTime parsedDate;
                    if (DateTime.TryParseExact(DateofOrder.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                    {
                        // Date parsed successfully
                        command.Parameters.AddWithValue("@Order_Date", parsedDate);
                    }
                    else
                    {
                        // Error message
                        MessageBox.Show("Invalid date format!");
                        return;
                    }

                    command.Parameters.AddWithValue("@Total_Order_Cost", decimal.Parse(TotalCost.Text));
                    command.Parameters.AddWithValue("@Client_Name", Clientname.Text);

                    command.ExecuteNonQuery();
                }
                MessageBox.Show("Order updated successfully!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating order: " + ex.Message);
            }
        }
        private void DateofOrder_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Перевірка, чи введено лише цифри або роздільники дати
            if (!char.IsDigit(e.Text[0]) && e.Text[0] != '.' && e.Text[0] != '-')
            {
                e.Handled = true; // Відхилення невідповідного символу
            }
        }


        private void btnClose1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
