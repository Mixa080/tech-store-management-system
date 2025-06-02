using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep.admin
{
    public class Product
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public string Model_Name { get; set; }
        public string Screen { get; set; }
        public string Processor { get; set; }
        public string Memory { get; set; }
        public string Camera { get; set; }
        public string Battery { get; set; }
        public string Diagonal { get; set; }
        public string Resolution { get; set; }
        public string Display_Type { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public decimal Price { get; set; }
        public int Quantity_In_Stock { get; set; }
        public int Warranty_Period { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }

        // Відсутні властивості
        public decimal TotalPrice => Price * Quantity;

        public int Quantity { get; set; } = 1;
        public string Description { get; set; }
        public string Brand_Name { get; set; }
        public string Category_Name { get; set; }
        public string Color_Name { get; set; }
        public int Product_ID { get; set; }
        public int Model_ID { get; set; }
        public int Brand_ID { get; set; }
        public int Category_ID { get; set; }
        public int Color_ID { get; set; }
    }

}
