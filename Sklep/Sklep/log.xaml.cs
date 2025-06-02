using System;
using System.Windows;
using System.Windows.Input;
using Sklep.ConnectionToSQL.Helper;
using Sklep.Pages;
using Sklep.User;
using Sklep.Pages;
namespace Sklep
{
    public partial class log : Window
    {
        private User.User currentUser;

        public log()
        {
            InitializeComponent();
            DBHelper.EstablishConnection();
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string u_login = txtUser.Text.Trim();
            string u_pass = txtPass.Password.Trim();

            // Перевірка на пусті поля логіну та паролю
            if (string.IsNullOrWhiteSpace(u_login) || string.IsNullOrWhiteSpace(u_pass))
            {
                MessageBox.Show("Поле для Email або пароля не може бути порожнім");
                return;
            }

            // Перевірка довжини паролю
            if (u_pass.Length < 3)
            {
                MessageBox.Show("Пароль занадто короткий. Введіть принаймні 3 символи");
                return;
            }

            // Отримання користувача з бази даних
            User.User aUser = UserDA.RetrieveUser(u_login);

            if (aUser != null && aUser.Password.Equals(u_pass, StringComparison.OrdinalIgnoreCase))
            {
                // Вхід в систему успішний
                MessageBox.Show("Ви успішно увійшли в систему!", "Успішний вхід", MessageBoxButton.OK, MessageBoxImage.Information);

                // Відкриття головного вікна (MainWindow)
                if (aUser.U_mode == "admin")
                {
                    currentUser = UserDA.RetrieveUser(u_login);
                    MainWindowAdmin mainWindowadmin = new MainWindowAdmin();
                    mainWindowadmin.Show();
                }
                else if (aUser.U_mode == "cass")
                {
                    currentUser = UserDA.RetrieveUser(u_login);
                    MessageBox.Show("Ви увійшли як касир", "Успішний вхід", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                else
                {
                    // Додано відлагодження для виведення значення режиму користувача, якщо воно відрізняється від "admin" або "cas"
                    MessageBox.Show($"Невідомий режим користувача: {aUser.U_mode}", "Успішний вхід", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                // Закриття поточного вікна логіну
                Close();
            }
            else
            {
                MessageBox.Show("Неправильний email або пароль. Будь ласка, спробуйте ще раз.", "Помилка входу", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

//form formWindow = new form(); // Assuming "form" is the name of your new window class
//formWindow.Show();

