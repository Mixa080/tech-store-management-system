using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Identity.Client.Extensions.Msal;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tsp;
using Sklep.admin;
using Sklep.Helper;

namespace Sklep.Pages
{
    public partial class Dashboard : Page
    {
        private List<Product> selectedProducts = new List<Product>();
        private decimal totalAmount = 0; // Додано нове поле для збереження загальної суми

        public Dashboard()
        {
            InitializeComponent();
            LoadProducts();
            FillComboBoxes();
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowState = WindowState.Maximized;

            }
            UpdateTotalAmount();
        }

        private void LoadProducts()
        {
            try
            {
                List<Product> allProducts = ProductOperationsHelper.GetAllProducts();
                ProductsListView.ItemsSource = allProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при завантаженні продуктів: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
   
            private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchKeyword = SearchTextBox.Text;
            string selectedBrand = cmbBrand.SelectedItem as string;
            string selectedColor = cmbColor.SelectedItem as string;
            string selectedCategory = cmbCategory.SelectedItem as string;

            decimal? minPrice = null;
            decimal? maxPrice = null;
            if (!string.IsNullOrWhiteSpace(txtMinPrice.Text))
            {
                if (decimal.TryParse(txtMinPrice.Text, out decimal min))
                {
                    minPrice = min;
                }
                else
                {
                    MessageBox.Show("Мінімальна ціна повинна бути числом.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(txtMaxPrice.Text))
            {
                if (decimal.TryParse(txtMaxPrice.Text, out decimal max))
                {
                    maxPrice = max;
                }
                else
                {
                    MessageBox.Show("Максимальна ціна повинна бути числом.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(txtMinPrice.Text))
            {
                if (decimal.TryParse(txtMinPrice.Text, out decimal min) && min > 0) // Додано перевірку на значення більше нуля
                {
                    minPrice = min;
                }
                else
                {
                    MessageBox.Show("Мінімальна ціна повинна бути числом більшим за нуль.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            if (!string.IsNullOrWhiteSpace(txtMaxPrice.Text))
            {
                if (decimal.TryParse(txtMaxPrice.Text, out decimal max) && max > 0) // Додано перевірку на значення більше нуля
                {
                    maxPrice = max;
                }
                else
                {
                    MessageBox.Show("Максимальна ціна повинна бути числом більшим за нуль.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            List<Product> searchResult = ProductOperationsHelper.SearchProduct(searchKeyword, selectedBrand, selectedColor, selectedCategory, minPrice, maxPrice);

            if (searchResult.Count > 0)
            {
                ProductsListView.ItemsSource = searchResult;
                MessageBox.Show("Пошук завершено.", "Результат пошуку", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Нічого не знайдено.", "Результат пошуку", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            ProductsListView.Items.Refresh();

            // Clear filters after search
            SearchTextBox.Text = string.Empty;
            cmbBrand.SelectedIndex = -1;
            cmbColor.SelectedIndex = -1;
            cmbCategory.SelectedIndex = -1;
            txtMinPrice.Text = string.Empty;
            txtMaxPrice.Text = string.Empty;
        }



        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product selectedProduct = (Product)ProductsListView.SelectedItem;

            if (selectedProduct != null)
            {
                if (selectedProduct.Quantity_In_Stock == 0)
                {
                    MessageBox.Show($"Товар \"{selectedProduct.Product_Name}\" недоступний для додавання до кошика, оскільки його кількість на складі дорівнює нулю.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    // Check if the product is already in the list
                    Product existingProduct = selectedProducts.Find(p => p.Product_ID == selectedProduct.Product_ID);
                    if (existingProduct != null)
                    {
                        existingProduct.Quantity++; // Increase the quantity in the basket
                    }
                    else
                    {
                        selectedProducts.Add(new Product
                        {
                            Product_ID = selectedProduct.Product_ID,
                            Product_Name = selectedProduct.Product_Name,
                            Model_Name = selectedProduct.Model_Name,
                            Description = selectedProduct.Description,
                            Price = selectedProduct.Price,
                            Quantity = 1,
                            Brand_Name = selectedProduct.Brand_Name,
                            Category_Name = selectedProduct.Category_Name,
                            Warranty_Period = selectedProduct.Warranty_Period,
                           
                            Color_Name = selectedProduct.Color_Name
                        });
                    }

                    selectedProduct.Quantity_In_Stock--; // Decrease the stock quantity by 1

                    MessageBox.Show($"Товар \"{selectedProduct.Product_Name}\" додано до кошика.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

                    ProductsListView.Items.Refresh();
                    basket.ItemsSource = null;
                    basket.ItemsSource = selectedProducts;
                    UpdateTotalAmount();

                }
            }
        }



        private void Basket_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Product selectedProduct = (Product)basket.SelectedItem;

            if (selectedProduct != null)
            {
                // Find the product in the original product list and increase the stock quantity
                Product productInList = ((List<Product>)ProductsListView.ItemsSource).Find(p => p.Product_ID == selectedProduct.Product_ID);
                if (productInList != null)
                {
                    productInList.Quantity_In_Stock++;
                }

                // Remove the product from the basket
                selectedProducts.Remove(selectedProduct);

                MessageBox.Show($"Товар \"{selectedProduct.Product_Name}\" видалено з кошика.", "Успіх", MessageBoxButton.OK, MessageBoxImage.Information);

                // Refresh the list views to reflect the changes
                ProductsListView.Items.Refresh();
                basket.ItemsSource = null;
                basket.ItemsSource = selectedProducts;
                if (selectedProducts.Count == 0)
                {
                    TotalAmountTextBlock.Text = "Загальна сума: 0";
                }
                UpdateTotalAmount();
                

            }
        }


        private void FillComboBoxes()
        {
            try
            {
                List<string> brands = ProductOperationsHelper.GetDistinctBrands();
                List<string> colors = ProductOperationsHelper.GetDistinctColors();
                List<string> categories = ProductOperationsHelper.GetDistinctCategories();

                cmbBrand.ItemsSource = brands;
                cmbColor.ItemsSource = colors;
                cmbCategory.ItemsSource = categories;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася помилка при завантаженні даних для комбобоксів: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void UpdateTotalAmount()
        {
            totalAmount = 0;
            foreach (var product in selectedProducts)
            {
                totalAmount += product.Price * product.Quantity;
            }

            TotalAmountTextBlock.Text = "Загальна сума: " + totalAmount.ToString("C");
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProducts.Count > 0)
            {
                Order order = new Order(selectedProducts);
                order.ShowDialog();
                selectedProducts.Clear();
                basket.ItemsSource = null;
                TotalAmountTextBlock.Text = "Загальна сума: 0";
            }
            else
            {
                MessageBox.Show("Виберіть товари для оформлення замовлення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void btninfo_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProducts.Count > 0)
            {
                ProductInfoWindow productInfoWindow = new ProductInfoWindow(selectedProducts[0]);
                productInfoWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Нет выбранных продуктов для отображения информации.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
