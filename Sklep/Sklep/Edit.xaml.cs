using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Identity.Client.Extensions.Msal;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tsp;
using Sklep.admin;

namespace Sklep
{
    public partial class Edit : Window
    {
        private Product selectedProduct;
        private string connectionString = "server=127.0.0.1;user=root;password=0000;database=shop";
        private List<TextBox> allTextBoxes;
        private ComboBox Specification;
        private string initialBrand;
        private string initialCategory;
        private string initialColor;
        public Edit(Product selectedProduct)
        {
            InitializeComponent();
            this.selectedProduct = selectedProduct;
            Loaded += Edit_Loaded;
            allTextBoxes = new List<TextBox>
{
     Screen, Processor, Memory, Camera, Battery,
    Diagonal, Resolution, Display_Type, RAM, Storage
};

            PopulateComboBoxesForProduct(); // Заменил на PopulateComboBoxesForProduct
            PopulateFields();
            PopulateModels(); // Добавляем вызов PopulateModels()
            PopulateComboBoxes(); // Добавил вызов PopulateComboBoxes
            SetComboBoxSelections(); // Добавил вызов PopulateComboBoxes
            DisableEmptyTextBoxes();

        }
        private void Edit_Loaded(object sender, RoutedEventArgs e)
        {
            // Присвоєння значень для комбобоксів
            Brand.SelectedItem = selectedProduct.Brand;
            Category.SelectedItem = selectedProduct.Category;
            Color.SelectedItem = selectedProduct.Color;

            Category_SelectionChanged(null, null);
        }
        private void DisableEmptyTextBoxes()
        {
            foreach (TextBox textBox in allTextBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.IsEnabled = false;
                }
            }
        }

        private void PopulateFields()
        {
            Product_Name.Text = selectedProduct.Product_Name;
            Model.Text = GetModelName(selectedProduct.Model_ID);
            Price.Text = selectedProduct.Price.ToString();
            Quantity_In_Stock.Text = selectedProduct.Quantity_In_Stock.ToString();
            Warranty_Period.Text = selectedProduct.Warranty_Period.ToString();
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


        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Category.SelectedItem != null)
            {
                string categoryName = Category.SelectedItem.ToString();

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

        private void EnableTextBoxes(params TextBox[] textBoxesToEnable)
        {
            foreach (TextBox textBox in allTextBoxes)
            {
                if (!textBoxesToEnable.Contains(textBox))
                {
                    textBox.Text = "";  // Очищення текстового поля перед його відключенням
                    textBox.IsEnabled = false;
                }
            }

            foreach (TextBox textBox in textBoxesToEnable)
            {
                textBox.IsEnabled = true;
            }
        }


        private void EnableAllTextBoxes()
        {
            foreach (TextBox textBox in allTextBoxes)
            {
                textBox.IsEnabled = true;
            }
        }

        private void DisableAllTextBoxes()
        {
            foreach (TextBox textBox in allTextBoxes)
            {
                textBox.IsEnabled = false;
            }
        }

        private void PopulateComboBoxes()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    PopulateComboBox("SELECT Brand_Name FROM Brand", Brand);
                    PopulateComboBox("SELECT Category_Name FROM Category", Category);
                    PopulateComboBox("SELECT Color_Name FROM Color", Color);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating combo boxes: " + ex.Message);
            }
        }

        private void PopulateComboBox(string query, ComboBox comboBox)
        {
            using (MySqlCommand command = new MySqlCommand(query, new MySqlConnection(connectionString)))
            {
                command.Connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                List<string> items = new List<string>();
                while (reader.Read())
                {
                    items.Add(reader[0].ToString());
                }
                comboBox.ItemsSource = items;
                reader.Close();
            }
        }
        private int GetModelIdForProduct(int productId)
        {
            int modelId1 = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Model_ID FROM Product WHERE Product_ID = @ProductId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductId", productId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            modelId1 = Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні ID моделі: {ex.Message}");
            }
            return modelId1;
        }

        private void SetComboBoxSelections()
        {
            initialBrand = selectedProduct.Brand;
            initialCategory = selectedProduct.Category;
            initialColor = selectedProduct.Color;

            Brand.SelectedItem = initialBrand;
            Category.SelectedItem = initialCategory;
            Color.SelectedItem = initialColor;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            selectedProduct.Product_Name = Product_Name.Text;
            selectedProduct.Screen = Screen.Text;
            selectedProduct.Processor = Processor.Text;
            selectedProduct.Memory = Memory.Text;
            selectedProduct.Camera = Camera.Text;
            selectedProduct.Battery = Battery.Text;
            selectedProduct.Diagonal = Diagonal.Text;
            selectedProduct.Resolution = Resolution.Text;
            selectedProduct.Display_Type = Display_Type.Text;
            selectedProduct.RAM = RAM.Text;
            selectedProduct.Storage = Storage.Text;
            selectedProduct.Price = decimal.Parse(Price.Text);
            selectedProduct.Quantity_In_Stock = int.Parse(Quantity_In_Stock.Text);
            selectedProduct.Warranty_Period = int.Parse(Warranty_Period.Text);
            if (Brand.SelectedItem != null && Brand.SelectedItem.ToString() != initialBrand)
            {
                selectedProduct.Brand = Brand.SelectedItem.ToString();
            }

            if (Category.SelectedItem != null && Category.SelectedItem.ToString() != initialCategory)
            {
                selectedProduct.Category = Category.SelectedItem.ToString();
            }

            if (Color.SelectedItem != null && Color.SelectedItem.ToString() != initialColor)
            {
                selectedProduct.Color = Color.SelectedItem.ToString();
            }
            // Отримуємо Model_ID окремо
            int modelId1 = GetModelIdForProduct(selectedProduct.Product_ID);

            // Оновлюємо назву моделі у базі даних
            string updatedModelName = Model.Text;
            UpdateModelName(modelId1, updatedModelName);

            // Оновлюємо інформацію про продукт у базі даних з використанням Model_ID
            selectedProduct.Model_ID = modelId1; // Переконайтесь, що Model_ID збережено правильно
            if (!CheckAllTextBoxesFilled())
            {
                MessageBox.Show("Будь ласка, заповніть усі поля перед додаванням продукту.");
                return;
            }
            foreach (var textBox in allTextBoxes)
            {
                // Предполагается, что имя технической характеристики совпадает с именем TextBox
                UpdateProductSpecification(selectedProduct.Product_ID, textBox.Name, textBox.Text);
            }

            UpdateProductInDatabase(selectedProduct);

            MessageBox.Show("Продукт успішно оновлено!");
            Close();
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
        private void UpdateProductInDatabase(Product product)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string updateProductQuery = "UPDATE product SET Product_Name = @ProductName, " +
                                                "Quantity_In_Stock = @QuantityInStock, Warranty_Period = @WarrantyPeriod, " +
                                                "Model_ID = @ModelId";

                    if (product.Brand != initialBrand)
                    {
                        updateProductQuery += ", Brand_ID = @BrandId";
                    }

                    if (product.Category != initialCategory)
                    {
                        updateProductQuery += ", Category_ID = @CategoryId";
                    }

                    if (product.Color != initialColor)
                    {
                        updateProductQuery += ", Color_ID = @ColorId";
                    }

                    updateProductQuery += " WHERE Product_ID = @ProductId";

                    using (MySqlCommand productCommand = new MySqlCommand(updateProductQuery, connection))
                    {
                        productCommand.Parameters.AddWithValue("@ProductName", product.Product_Name);
                        productCommand.Parameters.AddWithValue("@QuantityInStock", product.Quantity_In_Stock);
                        productCommand.Parameters.AddWithValue("@WarrantyPeriod", product.Warranty_Period);
                        productCommand.Parameters.AddWithValue("@ProductId", product.Product_ID);
                        productCommand.Parameters.AddWithValue("@ModelId", product.Model_ID);

                        if (product.Brand != initialBrand)
                        {
                            productCommand.Parameters.AddWithValue("@BrandId", GetBrandId(product.Brand));
                        }

                        if (product.Category != initialCategory)
                        {
                            productCommand.Parameters.AddWithValue("@CategoryId", GetCategoryId(product.Category));
                        }

                        if (product.Color != initialColor)
                        {
                            productCommand.Parameters.AddWithValue("@ColorId", GetColorId(product.Color));
                        }

                        productCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні продукту: {ex.Message}");
            }
        }





        private void UpdateProductSpecification(int productId, string specificationName, string value)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Перевіряємо наявність запису
                    string checkQuery = "SELECT COUNT(*) FROM Product_Specification_Value " +
                                        "WHERE Product_ID = @ProductId AND " +
                                        "Category_Specification_ID IN (SELECT Category_Specification_ID FROM Category_Specification " +
                                                                      "JOIN Technical_Specifications ON Category_Specification.Technical_Specifications_ID = Technical_Specifications.Technical_Specifications_ID " +
                                                                      "WHERE Specification_Name = @SpecificationName)";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@ProductId", productId);
                        checkCommand.Parameters.AddWithValue("@SpecificationName", specificationName);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                        if (count > 0)
                        {
                            // Оновлення існуючого запису
                            string updateQuery = "UPDATE Product_Specification_Value SET Description = @Description " +
                                                 "WHERE Product_ID = @ProductId AND " +
                                                 "Category_Specification_ID IN (SELECT cs.Category_Specification_ID " +
                                                                               "FROM Category_Specification cs " +
                                                                               "JOIN Technical_Specifications ts ON cs.Technical_Specifications_ID = ts.Technical_Specifications_ID " +
                                                                               "WHERE ts.Specification_Name = @SpecificationName)";
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@Description", value);
                                updateCommand.Parameters.AddWithValue("@ProductId", productId);
                                updateCommand.Parameters.AddWithValue("@SpecificationName", specificationName);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні технічної характеристики {specificationName}: {ex.Message}");
            }
        }


        private int GetCategorySpecificationId(string specificationName)
        {
            int categorySpecificationId = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT Category_Specification_ID FROM Category_Specification " +
                                                            "JOIN Technical_Specifications ON Category_Specification.Technical_Specifications_ID = Technical_Specifications.Technical_Specifications_ID " +
                                                            "WHERE Specification_Name = @SpecificationName", connection);
                    command.Parameters.AddWithValue("@SpecificationName", specificationName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        categorySpecificationId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні ID специфікації категорії: {ex.Message}");
            }
            return categorySpecificationId;
        }





        private int GetBrandId(string brandName)
        {
            int brandId = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT Brand_ID FROM Brand WHERE Brand_Name=@BrandName", connection);
                    command.Parameters.AddWithValue("@BrandName", brandName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        brandId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні ID бренду: {ex.Message}");
            }
            return brandId;
        }

        private int GetCategoryId(string categoryName)
        {
            int categoryId = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT Category_ID FROM Category WHERE Category_Name=@CategoryName", connection);
                    command.Parameters.AddWithValue("@CategoryName", categoryName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        categoryId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні ID категорії: {ex.Message}");
            }
            return categoryId;
        }

        private int GetColorId(string colorName)
        {
            int colorId = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT Color_ID FROM Color WHERE Color_Name=@ColorName", connection);
                    command.Parameters.AddWithValue("@ColorName", colorName);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        colorId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при отриманні ID кольору: {ex.Message}");
            }
            return colorId;
        }

        private string GetModelName(int modelId)
        {
            string modelName = "";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT Model_Name FROM Model WHERE Model_ID = @ModelId", connection);
                    command.Parameters.AddWithValue("@ModelId", modelId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        modelName = result.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении названия модели: " + ex.Message);
            }
            return modelName;
        }


        private void PopulateComboBoxesForProduct() // Змінено назву методу з PopulateComboBoxes на PopulateComboBoxesForProduct
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    PopulateComboBox("SELECT Brand_Name FROM Brand", Brand);
                    PopulateComboBox("SELECT Category_Name FROM Category", Category);
                    PopulateComboBox("SELECT Color_Name FROM Color", Color);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating combo boxes: " + ex.Message);
            }
        }

        private void PopulateModels()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Получаем ID модели из таблицы Product
                    int modelId = GetModelId(selectedProduct.Product_ID);
                    // Получаем название модели по ее ID
                    string modelName = GetModelName(modelId);
                    Model.Text = modelName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при заполнении моделей: " + ex.Message);
            }
        }
        private int GetModelId(int productId)
        {
            int modelId = -1;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT Model_ID FROM Product WHERE Product_ID = @ProductId", connection);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        modelId = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении ID модели: {ex.Message}");
            }
            return modelId;
        }
        private void PopulateSpecifications()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    PopulateComboBox("SELECT Specification_Name FROM Technical_Specifications", Specification);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error populating specifications: " + ex.Message);
            }
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

        private void UpdateModelName(int modelId1, string modelName)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string updateModelQuery = "UPDATE Model SET Model_Name = @ModelName WHERE Model_ID = @ModelId";
                    using (MySqlCommand command = new MySqlCommand(updateModelQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ModelName", modelName);
                        command.Parameters.AddWithValue("@ModelId", modelId1);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при оновленні назви моделі: {ex.Message}");
            }
        }



        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}