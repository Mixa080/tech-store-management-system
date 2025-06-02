using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;
using Sklep.admin;
using Sklep.ConnectionToSQL.Helper;

namespace Sklep.Helper
{
    public static class ProductOperationsHelper
    {
        public static List<Product> SearchProduct(string searchText, string selectedBrand, string selectedColor, string selectedCategory, decimal? minPrice, decimal? maxPrice)
        {
            List<Product> products = new List<Product>();

            try
            {
                DBHelper.EstablishConnection();

                if (DBHelper.connection != null)
                {
                    string query = @"
                    SELECT p.Product_ID, p.Product_Name, m.Model_Name, group_concat(psv.Description) AS Description, 
                           p.Price, p.Quantity_In_Stock, b.Brand_Name, c.Category_Name, p.Warranty_Period, 
                           cl.Color_Name
                    FROM Product p
                    LEFT JOIN Model m ON p.Model_ID = m.Model_ID
                    LEFT JOIN Product_Specification_Value psv ON p.Product_ID = psv.Product_ID
                    LEFT JOIN Category_Specification cs ON psv.Category_Specification_ID = cs.Category_Specification_ID
                    LEFT JOIN Category c ON p.Category_ID = c.Category_ID
                    LEFT JOIN Brand b ON p.Brand_ID = b.Brand_ID
                    LEFT JOIN Color cl ON p.Color_ID = cl.Color_ID
                    WHERE p.Product_Name LIKE @searchText";

                    if (!string.IsNullOrEmpty(selectedBrand))
                    {
                        query += " AND b.Brand_Name = @selectedBrand";
                    }

                    if (!string.IsNullOrEmpty(selectedColor))
                    {
                        query += " AND cl.Color_Name = @selectedColor";
                    }

                    if (!string.IsNullOrEmpty(selectedCategory))
                    {
                        query += " AND c.Category_Name = @selectedCategory";
                    }

                    if (minPrice.HasValue)
                    {
                        query += " AND p.Price >= @minPrice";
                    }

                    if (maxPrice.HasValue)
                    {
                        query += " AND p.Price <= @maxPrice";
                    }

                    query += @"
                    GROUP BY p.Product_ID, p.Product_Name, m.Model_Name, p.Price, p.Quantity_In_Stock, b.Brand_Name, 
                             c.Category_Name, p.Warranty_Period, cl.Color_Name";

                    using (MySqlCommand cmd = new MySqlCommand(query, DBHelper.connection))
                    {
                        cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                        if (!string.IsNullOrEmpty(selectedBrand))
                        {
                            cmd.Parameters.AddWithValue("@selectedBrand", selectedBrand);
                        }

                        if (!string.IsNullOrEmpty(selectedColor))
                        {
                            cmd.Parameters.AddWithValue("@selectedColor", selectedColor);
                        }

                        if (!string.IsNullOrEmpty(selectedCategory))
                        {
                            cmd.Parameters.AddWithValue("@selectedCategory", selectedCategory);
                        }

                        if (minPrice.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@minPrice", minPrice.Value);
                        }

                        if (maxPrice.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@maxPrice", maxPrice.Value);
                        }

                        DBHelper.connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    Product_ID = Convert.ToInt32(reader["Product_ID"]),
                                    Product_Name = reader["Product_Name"].ToString(),
                                    Model_Name = reader["Model_Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Quantity_In_Stock = Convert.ToInt32(reader["Quantity_In_Stock"]),
                                    Brand_Name = reader["Brand_Name"].ToString(),
                                    Category_Name = reader["Category_Name"].ToString(),
                                    Warranty_Period = Convert.ToInt32(reader["Warranty_Period"]),
                                    Color_Name = reader["Color_Name"].ToString()
                                };
                                products.Add(product);
                            }
                        }
                        DBHelper.connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DBHelper.connection?.Close();
            }

            return products;
        }

        public static List<string> GetDistinctBrands()
        {
            List<string> brands = new List<string>();

            try
            {
                DBHelper.EstablishConnection();

                if (DBHelper.connection != null)
                {
                    string query = "SELECT DISTINCT Brand_Name FROM Brand";

                    using (MySqlCommand cmd = new MySqlCommand(query, DBHelper.connection))
                    {
                        DBHelper.connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string brand = reader["Brand_Name"].ToString();
                                brands.Add(brand);
                            }
                        }
                        DBHelper.connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DBHelper.connection?.Close();
            }

            return brands;
        }

        public static List<string> GetDistinctColors()
        {
            List<string> colors = new List<string>();

            try
            {
                DBHelper.EstablishConnection();

                if (DBHelper.connection != null)
                {
                    string query = "SELECT DISTINCT Color_Name FROM Color";

                    using (MySqlCommand cmd = new MySqlCommand(query, DBHelper.connection))
                    {
                        DBHelper.connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string color = reader["Color_Name"].ToString();
                                colors.Add(color);
                            }
                        }
                        DBHelper.connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DBHelper.connection?.Close();
            }

            return colors;
        }

        public static List<string> GetDistinctCategories()
        {
            List<string> categories = new List<string>();

            try
            {
                DBHelper.EstablishConnection();

                if (DBHelper.connection != null)
                {
                    string query = "SELECT DISTINCT Category_Name FROM Category";

                    using (MySqlCommand cmd = new MySqlCommand(query, DBHelper.connection))
                    {
                        DBHelper.connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader["Category_Name"].ToString();
                                categories.Add(category);
                            }
                        }
                        DBHelper.connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DBHelper.connection?.Close();
            }

            return categories;
        }

        public static List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                DBHelper.EstablishConnection();

                if (DBHelper.connection != null)
                {
                    string query = @"
                        SELECT p.Product_ID, p.Product_Name, m.Model_Name, group_concat(psv.Description) AS Description, 
                               p.Price, p.Quantity_In_Stock, b.Brand_Name, c.Category_Name, p.Warranty_Period, 
                               cl.Color_Name
                        FROM Product p
                        LEFT JOIN Model m ON p.Model_ID = m.Model_ID
                        LEFT JOIN Product_Specification_Value psv ON p.Product_ID = psv.Product_ID
                        LEFT JOIN Category_Specification cs ON psv.Category_Specification_ID = cs.Category_Specification_ID
                        LEFT JOIN Category c ON p.Category_ID = c.Category_ID
                        LEFT JOIN Brand b ON p.Brand_ID = b.Brand_ID
                        LEFT JOIN Color cl ON p.Color_ID = cl.Color_ID
                        GROUP BY p.Product_ID, p.Product_Name, m.Model_Name, p.Price, p.Quantity_In_Stock, b.Brand_Name, 
                                 c.Category_Name, p.Warranty_Period, cl.Color_Name";

                    using (MySqlCommand cmd = new MySqlCommand(query, DBHelper.connection))
                    {
                        DBHelper.connection.Open();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    Product_ID = Convert.ToInt32(reader["Product_ID"]),
                                    Product_Name = reader["Product_Name"].ToString(),
                                    Model_Name = reader["Model_Name"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    Price = Convert.ToDecimal(reader["Price"]),
                                    Quantity_In_Stock = Convert.ToInt32(reader["Quantity_In_Stock"]),
                                    Brand_Name = reader["Brand_Name"].ToString(),
                                    Category_Name = reader["Category_Name"].ToString(),
                                    Warranty_Period = Convert.ToInt32(reader["Warranty_Period"]),
                                    Color_Name = reader["Color_Name"].ToString()
                                };
                                products.Add(product);
                            }
                        }
                        DBHelper.connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                DBHelper.connection?.Close();
            }

            return products;
        }
    }
}
