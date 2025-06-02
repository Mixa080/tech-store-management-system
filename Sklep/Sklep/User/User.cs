using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.User
{
    public class User
    {
        private string u_login;
        private string u_pass;
        private string u_mode;
        public User(string u_login, string u_pass, string u_mode)
        {
            UserName = u_login;
            Password = u_pass;
            this.u_mode = u_mode;
        }

        public String UserName
        {
            get { return u_login; }
            set { u_login = value; }
        }

        public String Password
        {
            get { return u_pass; }
            set { u_pass = value; }
        }
        public string U_mode
        {
            get { return u_mode; }
            set { u_mode = value; }
        }
    }
}
