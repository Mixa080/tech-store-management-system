using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.admin
{
    public class ord
    {
        public int Order_Number { get; set; }
        public DateTime Order_Date { get; set; }
        public decimal Total_Order_Cost { get; set; }
        public string Order_Status { get; set; }
        public string Payment_Method { get; set; }
        public string Payment_Status { get; set; }
        public string Client_Name { get; set; }
        public string Product_Name { get; set; }
        public int Quantity { get; set; }
        public decimal Sale_Price { get; set; }
    }

}
