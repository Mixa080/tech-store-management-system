using MySql.Data.MySqlClient;
using Sklep.ConnectionToSQL.Helper;
using System;
using System.Data;

namespace Sklep.User
{
    public static class UserDA
    {
        public static User RetrieveUser(string u_login)
        {
            MySqlCommand cmd = null;
            DataTable dt = new DataTable();
            MySqlDataAdapter sda = null;

            string query = "SELECT * FROM shop.employeelogin WHERE u_login = @username LIMIT 1";


            try
            {
                cmd = DBHelper.RunQuery(query, u_login);

                if (cmd != null)
                {
                    sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        string uName = dr["u_login"].ToString();
                        string password = dr["u_pass"].ToString();
                        string u_mode = dr["u_mode"].ToString();
                        return new User(uName, password, u_mode);
                    }
                }
            }
            catch (Exception ex)
            {
                // Обробка помилок, якщо потрібно
            }
            finally
            {
                if (cmd != null) cmd.Dispose();
                if (sda != null) sda.Dispose();
            }

            return null;
        }
    }
}
