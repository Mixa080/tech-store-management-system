using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Sklep.admin;
using System.Diagnostics;



namespace Sklep
{
    public partial class Order : Window
    {
        private List<Product> selectedProducts;

        public Order(List<Product> products)
        {
            InitializeComponent();
            selectedProducts = products;

            // Bind the list of Product objects to the DataGrid
            orderDataGrid.ItemsSource = selectedProducts;
        }

        private void SaveOrderToDatabase()
        {
            try
            {
                // Перевірка, чи обрані продукти
                if (selectedProducts.Count == 0)
                {
                    MessageBox.Show("Будь ласка, оберіть товари для замовлення.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string fullName = txtUser.Text.Trim();
                if (string.IsNullOrEmpty(fullName))
                {
                    MessageBox.Show("Please enter your full name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!string.IsNullOrEmpty(fullName) && !fullName.All(char.IsLetter)) // Перевірка, чи всі символи в рядку є буквами
                {
                    MessageBox.Show("ПІБ має містити тільки букви.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Підключення до бази даних
                using (MySqlConnection connection = new MySqlConnection("server=127.0.0.1;user=root;password=0000;database=shop"))
                {
                    connection.Open();

                    // Пошук або додавання клієнта
                    int clientId = FindOrAddClient(txtUser.Text, "", connection);

                    // Підготовка та виконання SQL-запиту для збереження замовлення
                    string orderQuery = "INSERT INTO Order_General_Info (Order_Date, Total_Order_Cost, Order_Status, Payment_Status, Client_ID) " +
                                        "VALUES (@Order_Date, @Total_Order_Cost, @Order_Status, @Payment_Status, @Client_ID)";
                    using (MySqlCommand orderCommand = new MySqlCommand(orderQuery, connection))
                    {
                        orderCommand.Parameters.AddWithValue("@Order_Date", DateTime.Now);

                        orderCommand.Parameters.AddWithValue("@Total_Order_Cost", CalculateTotalOrderCost());
                        orderCommand.Parameters.AddWithValue("@Order_Status", "New");
                       
                        orderCommand.Parameters.AddWithValue("@Payment_Status", "Paid");
                        orderCommand.Parameters.AddWithValue("@Client_ID", clientId);
                        orderCommand.ExecuteNonQuery();
                    }

                    // Отримання номеру останнього вставленого запису
                    int lastOrderId = GetLastInsertedOrderId(connection);

                    // Підготовка та виконання SQL-запиту для збереження деталей замовлення
                    string orderDetailsQuery = "INSERT INTO Order_Details (Order_Number, Product_ID, Quantity) VALUES (@Order_Number, @Product_ID, @Quantity)";
                    foreach (var product in selectedProducts)
                    {
                        using (MySqlCommand detailsCommand = new MySqlCommand(orderDetailsQuery, connection))
                        {
                            detailsCommand.Parameters.AddWithValue("@Order_Number", lastOrderId);
                            detailsCommand.Parameters.AddWithValue("@Product_ID", product.Product_ID);
                            detailsCommand.Parameters.AddWithValue("@Quantity", product.Quantity);
                            detailsCommand.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Order successfully added to the database.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Генерування та відкриття чека
                    GenerateReceiptDocument(lastOrderId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving the order: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private int GetLastInsertedOrderId(MySqlConnection connection)
        {
            int lastOrderId = 0;
            string query = "SELECT LAST_INSERT_ID()";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    lastOrderId = Convert.ToInt32(result);
                }
            }
            return lastOrderId;
        }
        private void GenerateReceiptDocument(int orderId)
        {
            try
            {
                // Create the PDF document
                string pdfFilePath = "Receipt_" + orderId + ".pdf";
                using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);

                    // Open the document for editing
                    pdfDoc.Open();

                    // Add text and order details to the document
                    Font headerFont = new Font(Font.UNDEFINED, 16, Font.BOLD);// Removed FontFamily
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Order Number: " + orderId, headerFont));
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Order Date: " + DateTime.Now.ToString("dd/MM/yyyy"), headerFont));

                    // Add a table for the product details
                    PdfPTable table = new PdfPTable(3); // 3 columns for Product Name, Quantity, and Price
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 4, 2, 2 }); // Relative column widths

                    // Add table headers
                    PdfPCell cell = new PdfPCell(new Phrase("Product Name", headerFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Quantity", headerFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Price", headerFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);

                    // Add product details to the table
                    foreach (var product in selectedProducts)
                    {
                        // Changed to the actual property for product name
                        table.AddCell(product.Product_Name); // Assuming 'Name' is the property for product name
                        table.AddCell(product.Quantity.ToString());
                        table.AddCell(product.Price.ToString());
                    }

                    pdfDoc.Add(table);

                    // Calculate and add the total cost
                    decimal totalCost = CalculateTotalOrderCost();
                    pdfDoc.Add(new iTextSharp.text.Paragraph("Total Cost: " + totalCost.ToString(), headerFont));

                    // Close the document
                    pdfDoc.Close();
                    this.Close();
                    
                }


                // Specify the path to Adobe Acrobat Reader
                string acrobatReaderPath = @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe";

                // Check if Adobe Acrobat Reader exists
                if (File.Exists(acrobatReaderPath))
                {
                    // Start Adobe Acrobat Reader and open the PDF file
                    System.Diagnostics.Process.Start(acrobatReaderPath, pdfFilePath);
                }
                else
                {
                    MessageBox.Show("Adobe Acrobat Reader is not installed or the path is incorrect.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generating receipt document: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private int FindOrAddClient(string fullName, string contactNumber, MySqlConnection connection)
        {
            int clientId = FindClientId(fullName, connection);
            if (clientId == 0)
            {
                clientId = AddNewClient(fullName, contactNumber, connection);
            }
            return clientId;
        }

        private int FindClientId(string fullName, MySqlConnection connection)
        {
            int clientId = 0;
            string query = "SELECT Client_ID FROM Client WHERE Full_Name = @Full_Name";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Full_Name", fullName);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    clientId = Convert.ToInt32(result);
                }
            }
            return clientId;
        }

        private int AddNewClient(string fullName, string contactNumber, MySqlConnection connection)
        {
            int clientId = 0;
            string query = "INSERT INTO Client (Full_Name, Contact_Number) VALUES (@Full_Name, @Contact_Number); SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Full_Name", fullName);
                command.Parameters.AddWithValue("@Contact_Number", contactNumber);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    clientId = Convert.ToInt32(result);
                }
            }
            return clientId;
        }

        private decimal CalculateTotalOrderCost()
        {
            decimal totalCost = 0;
            foreach (var product in selectedProducts)
            {
                totalCost += product.Price * product.Quantity; // Умножаем цену продукта на его количество
            }
            return totalCost;
        }


        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            SaveOrderToDatabase();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
