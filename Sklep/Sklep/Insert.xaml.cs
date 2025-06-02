using Microsoft.Identity.Client.Extensions.Msal;

using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tsp;
using Sklep.admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;


namespace Sklep
{
    public partial class Insert : Window
    {
        private MySqlConnection connection = new MySqlConnection("server=127.0.0.1;user=root;password=0000;database=shop");
        List<TextBox> allTextBoxes = new List<TextBox>();

        public Insert()
        {
            InitializeComponent();
            FillComboBoxes();
            allTextBoxes.Add(Screen);
            allTextBoxes.Add(Processor);
            allTextBoxes.Add(Memory);
            allTextBoxes.Add(Camera);
            allTextBoxes.Add(Battery);
            allTextBoxes.Add(Diagonal);
            allTextBoxes.Add(Resolution);
            allTextBoxes.Add(Display_Type);
            allTextBoxes.Add(RAM);
            allTextBoxes.Add(Storage);
            Category.SelectionChanged += Category_SelectionChanged;
        }

        private void FillComboBoxes()
        {
            try
            {
                connection.Open();

                // Populate Brand ComboBox using MySqlDataAdapter
                string brandQuery = "SELECT Brand_ID, Brand_Name FROM Brand";
                MySqlDataAdapter brandAdapter = new MySqlDataAdapter(brandQuery, connection);
                DataTable brandTable = new DataTable();
                brandAdapter.Fill(brandTable);

                foreach (DataRow row in brandTable.Rows)
                {
                    Brand.Items.Add(new ComboBoxItem
                    {
                        Content = row["Brand_Name"],
                        Tag = row["Brand_ID"]
                    });
                }

                // Close the brandAdapter since it's no longer needed
                brandAdapter.Dispose();

                // Populate Color ComboBox using MySqlDataReader
                string colorQuery = "SELECT Color_ID, Color_Name FROM Color";
                MySqlCommand colorCommand = new MySqlCommand(colorQuery, connection);
                MySqlDataReader colorReader = colorCommand.ExecuteReader();

                while (colorReader.Read())
                {
                    Color.Items.Add(new ComboBoxItem
                    {
                        Content = colorReader["Color_Name"],
                        Tag = colorReader["Color_ID"]
                    });
                }
                colorReader.Close();

                // Populate Category ComboBox using CategoryModel objects
                string categoryQuery = "SELECT Category_ID, Category_Name FROM Category";
                MySqlDataAdapter categoryAdapter = new MySqlDataAdapter(categoryQuery, connection);
                DataTable categoryTable = new DataTable();
                categoryAdapter.Fill(categoryTable);

                Category.Items.Clear();

                foreach (DataRow row in categoryTable.Rows)
                {
                    CategoryModel category = new CategoryModel
                    {
                        Category_ID = Convert.ToInt32(row["Category_ID"]),
                        Category_Name = row["Category_Name"].ToString()
                    };

                    ComboBoxItem item = new ComboBoxItem
                    {
                        Content = category.Category_Name,
                        Tag = category.Category_ID
                    };
                    Category.Items.Add(item);
                }

                categoryAdapter.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Category.SelectedItem != null)
            {
                ComboBoxItem selectedItem = (ComboBoxItem)Category.SelectedItem;
                string categoryName = selectedItem.Content.ToString();

                DisableAllTextBoxes();

                switch (categoryName)
                {
                    case "Телевізори":
                        EnableTextBoxes(Diagonal, Display_Type);
                        break;
                    case "Побутова техніка":
                        EnableTextBoxes(Memory, Battery, Diagonal);
                        break;
                    case "Аксесуари":
                        EnableTextBoxes(Memory, Camera);
                        break;
                    case "Ігрові консолі":
                        EnableTextBoxes(Memory, RAM);
                        break;
                    case "Інше":
                        EnableTextBoxes(Processor, Memory, Camera, Battery, Diagonal, Resolution, Display_Type, RAM, Storage);
                        break;
                    default:
                        // Если выбранная категория не требует ввода значений, оставляем поля разблокированными
                        EnableAllTextBoxes();
                        break;
                }
            }
        }


        private void EnableAllTextBoxes()
        {
            foreach (TextBox textBox in allTextBoxes)
            {
                textBox.IsEnabled = true;
            }
        }


        private void EnableTextBoxes(params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in allTextBoxes)
            {
                textBox.IsEnabled = false;
            }

            foreach (TextBox textBox in textBoxes)
            {
                textBox.IsEnabled = true;
            }
        }

        private void DisableAllTextBoxes()
        {
            foreach (TextBox textBox in allTextBoxes)
            {
                if (!textBox.IsEnabled)
                {
                    textBox.Clear(); // Очищаем содержимое текстового поля только если оно было заблокировано
                }
                textBox.IsEnabled = false;
            }
        }


        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();

                string productName = Product_Name.Text;
                string model = Model.Text;
                string price = Price.Text;
                string quantityInStock = Quantity_In_Stock.Text;
                string brand = Brand.Text;
                string category = Category.Text;
                string warrantyPeriod = Warranty_Period.Text;
                string color = Color.Text;

                // Insert data into Model table
                string sqlInsertModel = @"
            INSERT INTO Model (Model_Name) 
            VALUES (@model);
        ";

                using (MySqlCommand cmd = new MySqlCommand(sqlInsertModel, connection))
                {
                    cmd.Parameters.AddWithValue("@model", model);
                    cmd.ExecuteNonQuery();
                }

                // Get the ID of the inserted model
                string sqlSelectModelId = "SELECT LAST_INSERT_ID();";
                int modelId;
                using (MySqlCommand cmd = new MySqlCommand(sqlSelectModelId, connection))
                {
                    modelId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Insert data into Product table
                string sqlInsertProduct = @"
            INSERT INTO Product (Product_Name, Model_ID, Price, Quantity_In_Stock, Brand_ID, Category_ID, Warranty_Period, Color_ID) 
            VALUES (@productName, @modelId, @price, @quantityInStock, @brandId, @categoryId, @warrantyPeriod, @colorId);
        ";

                using (MySqlCommand cmd = new MySqlCommand(sqlInsertProduct, connection))
                {
                    cmd.Parameters.AddWithValue("@productName", productName);
                    cmd.Parameters.AddWithValue("@modelId", modelId);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@quantityInStock", quantityInStock);
                    cmd.Parameters.AddWithValue("@brandId", GetIdFromName("Brand", brand));
                    cmd.Parameters.AddWithValue("@categoryId", GetIdFromName("Category", category));
                    cmd.Parameters.AddWithValue("@warrantyPeriod", warrantyPeriod);
                    cmd.Parameters.AddWithValue("@colorId", GetIdFromName("Color", color));
                    cmd.ExecuteNonQuery();
                }

                // Get the ID of the inserted product
                string sqlSelectProductId = "SELECT LAST_INSERT_ID();";
                int productId;
                using (MySqlCommand cmd = new MySqlCommand(sqlSelectProductId, connection))
                {
                    productId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Insert product specifications
                InsertProductSpecifications(productId);
                if (!CheckAllTextBoxesFilled())
                {
                    MessageBox.Show("Будь ласка, заповніть усі поля перед додаванням продукту.");
                    return;
                }
                MessageBox.Show("Продукт та його характеристики успішно додано!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при додаванні продукту та його характеристик: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

       
        private int GetIdFromName(string tableName, string name)
        {
            string sqlQuery = $"SELECT {tableName}_ID FROM {tableName} WHERE {tableName}_Name = @name;";
            using (MySqlCommand cmd = new MySqlCommand(sqlQuery, connection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void btnClose1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private int InsertProductSpecifications(int productId)
        {
            int productSpecValueId = 0;

            string sqlInsertSpecifications = @"
        INSERT INTO Product_Specification_Value (Product_ID, Category_Specification_ID, Description)
        VALUES
            (@productId, @categorySpecId1, @screen),
            (@productId, @categorySpecId2, @processor),
            (@productId, @categorySpecId3, @memory),
            (@productId, @categorySpecId4, @camera),
            (@productId, @categorySpecId5, @battery),
            (@productId, @categorySpecId6, @diagonal),
            (@productId, @categorySpecId7, @resolution),
            (@productId, @categorySpecId8, @displayType),
            (@productId, @categorySpecId9, @ram),
            (@productId, @categorySpecId10, @storage);
    ";

            using (MySqlCommand cmd = new MySqlCommand(sqlInsertSpecifications, connection))
            {
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@categorySpecId1", GetCategorySpecificationId("Екран"));
                cmd.Parameters.AddWithValue("@categorySpecId2", GetCategorySpecificationId("Процесор"));
                cmd.Parameters.AddWithValue("@categorySpecId3", GetCategorySpecificationId("Пам'ять"));
                cmd.Parameters.AddWithValue("@categorySpecId4", GetCategorySpecificationId("Камера"));
                cmd.Parameters.AddWithValue("@categorySpecId5", GetCategorySpecificationId("Батарея"));
                cmd.Parameters.AddWithValue("@categorySpecId6", GetCategorySpecificationId("Діагональ"));
                cmd.Parameters.AddWithValue("@categorySpecId7", GetCategorySpecificationId("Роздільна здатність"));
                cmd.Parameters.AddWithValue("@categorySpecId8", GetCategorySpecificationId("Тип дисплею"));
                cmd.Parameters.AddWithValue("@categorySpecId9", GetCategorySpecificationId("ОЗП"));
                cmd.Parameters.AddWithValue("@categorySpecId10", GetCategorySpecificationId("Накопичувач"));
                cmd.Parameters.AddWithValue("@screen", Screen.Text);
                cmd.Parameters.AddWithValue("@processor", Processor.Text);
                cmd.Parameters.AddWithValue("@memory", Memory.Text);
                cmd.Parameters.AddWithValue("@camera", Camera.Text);
                cmd.Parameters.AddWithValue("@battery", Battery.Text);
                cmd.Parameters.AddWithValue("@diagonal", Diagonal.Text);
                cmd.Parameters.AddWithValue("@resolution", Resolution.Text);
                cmd.Parameters.AddWithValue("@displayType", Display_Type.Text);
                cmd.Parameters.AddWithValue("@ram", RAM.Text);
                cmd.Parameters.AddWithValue("@storage", Storage.Text);
                cmd.ExecuteNonQuery();
            }

            // Get the ID of the inserted Product_Specification_Value
            string sqlSelectSpecsId = "SELECT LAST_INSERT_ID();";
            using (MySqlCommand cmd = new MySqlCommand(sqlSelectSpecsId, connection))
            {
                productSpecValueId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return productSpecValueId;
        }

        private int GetCategorySpecificationId(string specificationName)
        {
            int categorySpecificationId = -1;
            try
            {
                switch (specificationName)
                {
                    case "Екран":
                        categorySpecificationId = 1;
                        break;
                    case "Процесор":
                        categorySpecificationId = 2;
                        break;
                    case "Пам'ять":
                        categorySpecificationId = 3;
                        break;
                    case "Камера":
                        categorySpecificationId = 4;
                        break;
                    case "Батарея":
                        categorySpecificationId = 5;
                        break;
                    case "Діагональ":
                        categorySpecificationId = 6;
                        break;
                    case "Роздільна здатність":
                        categorySpecificationId = 7;
                        break;
                    case "Тип дисплею":
                        categorySpecificationId = 8;
                        break;
                    case "ОЗП":
                        categorySpecificationId = 9;
                        break;
                    case "Накопичувач":
                        categorySpecificationId = 10;
                        break;
                    // Add other cases for other specifications
                    default:
                        // Default value if specification is not found
                        categorySpecificationId = -1;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні ID специфікації категорії: {ex.Message}");
            }
            return categorySpecificationId;
        }
        private bool CheckAllTextBoxesFilled()
        {
            foreach (TextBox textBox in allTextBoxes)
            {
                if (textBox.IsEnabled && string.IsNullOrWhiteSpace(textBox.Text))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
