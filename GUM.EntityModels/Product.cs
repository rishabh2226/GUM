using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUM.ViewModels
{
    public class Product
    {

        public long ProductID { get; set; }
        public long SubcategoryID { get; set; }
        public string SubcategoryName { get; set; }
        public long CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public string Color { get; set; }
        public decimal DiscountPercentage { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Stock> Stocks { get; set; }
        public string[] Images { get; set; }

        public int TotalQuantity { get; set; }




    }
}
