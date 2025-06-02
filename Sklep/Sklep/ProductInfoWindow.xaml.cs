using MySql.Data.MySqlClient;
using Sklep.admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sklep
{
    /// <summary>
    /// Interaction logic for ProductInfoWindow.xaml
    /// </summary>
    public partial class ProductInfoWindow : Window
    {
        private string connectionString = "server=127.0.0.1;user=root;password=0000;database=shop";

        public ProductInfoWindow(Product selectedProduct)
        {
            InitializeComponent();
            PopulateFields(selectedProduct);
        }

        private void PopulateFields(Product selectedProduct)
        {
            Product_Name.Text = selectedProduct.Product_Name;
            Model.Text = selectedProduct.Model_Name;
            Screen.Text = GetSpecificationValue(selectedProduct.Product_ID, "Screen");
            Processor.Text = GetSpecificationValue(selectedProduct.Product_ID, "Processor");
            Memory.Text = GetSpecificationValue(selectedProduct.Product_ID, "Memory");
            Camera.Text = GetSpecificationValue(selectedProduct.Product_ID, "Camera");
            Battery.Text = GetSpecificationValue(selectedProduct.Product_ID, "Battery");
            Diagonal.Text = GetSpecificationValue(selectedProduct.Product_ID, "Diagonal");
            Resolution.Text = GetSpecificationValue(selectedProduct.Product_ID, "Resolution");
            Display_Type.Text = GetSpecificationValue(selectedProduct.Product_ID, "Display_Type");
            RAM.Text = GetSpecificationValue(selectedProduct.Product_ID, "RAM");
            Storage.Text = GetSpecificationValue(selectedProduct.Product_ID, "Storage");

            // Получаем идентификаторы категории, цвета и бренда из таблицы Product
            int categoryId = GetCategoryId(selectedProduct.Product_ID);
            int colorId = GetColorId(selectedProduct.Product_ID);
            int brandId = GetBrandId(selectedProduct.Product_ID);

            // Получаем названия категории, цвета и бренда из соответствующих таблиц
            Category.Text = GetCategoryName(categoryId);
            Color.Text = GetColorName(colorId);
            Warranty_Period.Text = selectedProduct.Warranty_Period.ToString();
            Price.Text = selectedProduct.Price.ToString();
            Quantity_In_Stock.Text = selectedProduct.Quantity_In_Stock.ToString();
            Brand.Text = GetBrandName(brandId);
        }


        private string GetSpecificationValue(int productId, string specificationName)
        {
            string value = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Category_Specification_ID FROM Category_Specification " +
                                   "JOIN Technical_Specifications ON Category_Specification.Technical_Specifications_ID = Technical_Specifications.Technical_Specifications_ID " +
                                   "WHERE Specification_Name = @SpecificationName LIMIT 1"; // Додали LIMIT 1
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SpecificationName", specificationName);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            int categorySpecificationId = Convert.ToInt32(result);

                            // Отримуємо значення за вказаним ідентифікатором
                            query = "SELECT Description FROM Product_Specification_Value WHERE Product_ID = @ProductId AND Category_Specification_ID = @CategorySpecificationId";
                            using (MySqlCommand valueCommand = new MySqlCommand(query, connection))
                            {
                                valueCommand.Parameters.AddWithValue("@ProductId", productId);
                                valueCommand.Parameters.AddWithValue("@CategorySpecificationId", categorySpecificationId);
                                object valueResult = valueCommand.ExecuteScalar();
                                if (valueResult != null)
                                {
                                    value = valueResult.ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні значення технічної характеристики {specificationName}: {ex.Message}");
            }
            return value;
        }
        private int GetCategoryId(int productId)
        {
            int categoryId = -1; // Значение по умолчанию, если идентификатор не найден
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Category_ID FROM Product WHERE Product_ID = @ProductId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            categoryId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении идентификатора категории: {ex.Message}");
            }
            return categoryId;
        }

        private int GetColorId(int productId)
        {
            int colorId = -1; // Значение по умолчанию, если идентификатор не найден
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Color_ID FROM Product WHERE Product_ID = @ProductId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            colorId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении идентификатора цвета: {ex.Message}");
            }
            return colorId;
        }

        private int GetBrandId(int productId)
        {
            int brandId = -1; // Значение по умолчанию, если идентификатор не найден
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Brand_ID FROM Product WHERE Product_ID = @ProductId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            brandId = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении идентификатора бренда: {ex.Message}");
            }
            return brandId;
        }

        private string GetCategoryName(int categoryId)
        {
            string categoryName = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Category_Name FROM Category WHERE Category_ID = @CategoryId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CategoryId", categoryId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            categoryName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении названия категории: {ex.Message}");
            }
            return categoryName;
        }

        private string GetColorName(int colorId)
        {
            string colorName = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Color_Name FROM Color WHERE Color_ID = @ColorId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ColorId", colorId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            colorName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении названия цвета: {ex.Message}");
            }
            return colorName;
        }

        private string GetBrandName(int brandId)
        {
            string brandName = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Brand_Name FROM Brand WHERE Brand_ID = @BrandId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BrandId", brandId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            brandName = result.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении названия бренда: {ex.Message}");
            }
            return brandName;
        }


        private void btnClose1_Click(object sender, RoutedEventArgs e)
        {
            Close();    
        }
    }
}
