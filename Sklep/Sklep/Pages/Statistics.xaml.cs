using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using MySql.Data.MySqlClient;
using Sklep.admin;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Sklep.Pages
{
    public partial class Statistics : Page
    {
        private List<SalesStatistic> statistics = new List<SalesStatistic>(); // Оголошення списку статистики
        private int totalQuantity = 0; // Оголошення загальної кількості
        private decimal totalPrice = 0;
        public Statistics()
        {
            InitializeComponent();
            PopulateComboBoxes();

        }

        private void PopulateComboBoxes()
        {
            // Заповнення комбобоксу з брендами
            brandComboBox.Items.Add("All Brands"); // Додайте варіант "Обрати все"
            List<string> brands = GetDistinctValues("Brand", "Brand_Name");
            foreach (string brand in brands)
            {
                brandComboBox.Items.Add(brand);
            }

            // Заповнення комбобоксу з моделями
            modelComboBox.Items.Add("All Models"); // Додайте варіант "Обрати все"
            List<string> models = GetDistinctValues("Model", "Model_Name");
            foreach (string model in models)
            {
                modelComboBox.Items.Add(model);
            }

            // Заповнення комбобоксу з категоріями
            categoryComboBox.Items.Add("All Categories"); // Додайте варіант "Обрати все"
            List<string> categories = GetDistinctValues("Category", "Category_Name");
            foreach (string category in categories)
            {
                categoryComboBox.Items.Add(category);
            }
        }

        private List<string> GetDistinctValues(string tableName, string columnName)
        {
            List<string> distinctValues = new List<string>();

            try
            {
                using (var connection = new MySqlConnection("server=127.0.0.1;user=root;password=0000;database=shop"))
                {
                    connection.Open();
                    string query = $"SELECT DISTINCT {columnName} FROM {tableName}";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string value = reader.GetString(0);
                            distinctValues.Add(value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving distinct values: " + ex.Message);
            }

            return distinctValues;
        }
        private void ClearBrandComboBox_Click(object sender, RoutedEventArgs e)
        {
            brandComboBox.SelectedIndex = -1; // Встановлюємо вибір на перший елемент (Обрати все)
        }

        private void ClearModelComboBox_Click(object sender, RoutedEventArgs e)
        {
            modelComboBox.SelectedIndex = -1; // Встановлюємо вибір на перший елемент (Обрати все)
        }

        private void ClearCategoryComboBox_Click(object sender, RoutedEventArgs e)
        {
            categoryComboBox.SelectedIndex = -1; // Встановлюємо вибір на перший елемент (Обрати все)
        }
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            // Reset total values
            totalQuantity = 0;
            totalPrice = 0;

            // Get selected values from ComboBoxes
            string selectedBrand = brandComboBox.SelectedItem as string;
            string selectedModel = modelComboBox.SelectedItem as string;
            string selectedCategory = categoryComboBox.SelectedItem as string;

            // Build query with conditional filtering
            string query = @"
    SELECT 
        P.Product_Name,
        B.Brand_Name,
        M.Model_Name,
        C.Category_Name,
        SUM(OD.Quantity) AS Quantity_Sold,
        P.Price
    FROM 
        Order_General_Info OG
    JOIN 
        Order_Details OD ON OG.Order_Number = OD.Order_Number
    JOIN 
        Product P ON OD.Product_ID = P.Product_ID
    JOIN 
        Model M ON P.Model_ID = M.Model_ID
    JOIN 
        Brand B ON P.Brand_ID = B.Brand_ID
    JOIN 
        Category C ON P.Category_ID = C.Category_ID
    WHERE 
        OG.Order_Date BETWEEN @StartDate AND @EndDate
        AND (@BrandName IS NULL OR B.Brand_Name = @BrandName)
        AND (@ModelName IS NULL OR M.Model_Name = @ModelName)
        AND (@CategoryName IS NULL OR C.Category_Name = @CategoryName)
    GROUP BY 
        P.Product_Name, B.Brand_Name, M.Model_Name, C.Category_Name, P.Price
    ORDER BY 
        Quantity_Sold DESC";

            try
            {
                using (var connection = new MySqlConnection("server=127.0.0.1;user=root;password=0000;database=shop"))
                {
                    connection.Open();

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StartDate", startDatePicker.SelectedDate ?? DateTime.MinValue);
                    command.Parameters.AddWithValue("@EndDate", endDatePicker.SelectedDate ?? DateTime.MaxValue);
                    command.Parameters.AddWithValue("@BrandName", selectedBrand == "All Brands" ? (object)DBNull.Value : selectedBrand);
                    command.Parameters.AddWithValue("@ModelName", selectedModel == "All Models" ? (object)DBNull.Value : selectedModel);
                    command.Parameters.AddWithValue("@CategoryName", selectedCategory == "All Categories" ? (object)DBNull.Value : selectedCategory);

                    using (var reader = command.ExecuteReader())
                    {
                        List<SalesStatistic> allStatistics = new List<SalesStatistic>(); // Create a list to hold all sales statistics
                        while (reader.Read())
                        {
                            SalesStatistic statistic = new SalesStatistic
                            {
                                Product_Name = reader.GetString(0),
                                Brand_Name = reader.GetString(1),
                                Model_Name = reader.GetString(2),
                                Category_Name = reader.GetString(3),
                                Quantity_Sold = reader.GetInt32(4),
                                Price = reader.GetDecimal(5)
                            };
                            statistics.Add(statistic); // Add individual sales statistic to the original list

                            totalQuantity += statistic.Quantity_Sold;
                            totalPrice += statistic.Quantity_Sold * statistic.Price;

                            allStatistics.Add(statistic); // Add individual sales statistic to the combined list
                        }

                        // Add total row to the list
                        allStatistics.Add(new SalesStatistic
                        {
                            Product_Name = "Total",
                            Quantity_Sold = totalQuantity,
                            Price = totalPrice
                        });

                        salesListView.ItemsSource = allStatistics; // Set the combined list as the ItemsSource
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating report: " + ex.Message);
            }
        }



    }

}