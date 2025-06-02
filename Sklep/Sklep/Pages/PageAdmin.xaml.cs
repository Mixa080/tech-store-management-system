using Sklep.admin;
using Sklep.Helper;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sklep.Pages
{
    /// <summary>
    /// Interaction logic for PageAdmin.xaml
    /// </summary>
    public partial class PageAdmin : Page
    {

        private List<Product> selectedProducts = new List<Product>(); // Declare the selectedProducts list here

        public PageAdmin()
        {

            InitializeComponent();
            FillComboBoxes();
            LoadProducts(); // Завантажуємо всі продукти при створенні вікна
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.WindowState = WindowState.Maximized;
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



        private List<string> GetSelectedCheckBoxes(StackPanel stackPanel)
        {
            List<string> selectedItems = new List<string>();
            foreach (var item in stackPanel.Children)
            {
                if (item is CheckBox checkBox && checkBox.IsChecked == true)
                {
                    selectedItems.Add(checkBox.Content.ToString());
                }
            }
            return selectedItems;
        }



        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Отримати вибраний об'єкт
            var selectedProduct = ProductsListView.SelectedItem as Sklep.admin.Product;

            // Перевірка, чи об'єкт не є нульовим
            if (selectedProduct != null)
            {
                // Створити нове вікно Edit і передати вибраний об'єкт як параметр
                Edit editWindow = new Edit(selectedProduct);
                editWindow.Show();
            }
        }



        private void btnADD_Click(object sender, RoutedEventArgs e)
        {
           
                
                Insert insert = new Insert();
                insert.ShowDialog();
            
          
        }
    }
}

