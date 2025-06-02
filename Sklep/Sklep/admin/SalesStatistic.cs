using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.admin
{
    public class SalesStatistic
    {
        public string Product_Name { get; set; }
        public int Quantity_Sold { get; set; }
        public string Brand_Name { get; set; }
        public string Model_Name { get; set; }
        public string Category_Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalCost { get { return Quantity_Sold * Price; } }

    }
    public class TotalSalesStatistic
    {
        public string Product_Name { get; set; }
        public int Quantity_Sold { get; set; }
        public decimal Price { get; set; }
    }
    public class BasketItem
    {
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Price * Quantity;
    }

}
