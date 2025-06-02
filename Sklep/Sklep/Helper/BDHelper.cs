using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sklep.ConnectionToSQL.Helper
{
    public static class DBHelper
    {
        // Змінено рівень доступу до поля connection на public
        public static MySqlConnection connection;
        private static MySqlCommand cmd = null;
        private static DataTable dt;
        private static MySqlDataAdapter sda;

        /*
         * Job is to establish connection to the database
         */
        public static void EstablishConnection()
        {
            try
            {
                MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
                builder.Server = "127.0.0.1";
                builder.UserID = "root";
                builder.Password = "0000";
                builder.Database = "shop";
                builder.SslMode = MySqlSslMode.Disabled; // Використовуйте MySqlSslMode.Disabled замість MySqlSslMode.None, як застаріле значення
                connection = new MySqlConnection(builder.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Failed: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static MySqlCommand RunQuery(string query, string u_login)
        {
            try
            {
                if (connection != null)
                {
                    connection.Open();
                    cmd = connection.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@username", u_login);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                connection.Close();
            }
            return cmd;
        }

        // Додамо доступний метод для отримання з'єднання
        public static MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
