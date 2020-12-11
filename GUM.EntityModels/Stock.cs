using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUM.ViewModels
{
    public class Stock
    {
        public long ProductID { get; set; }
        public long SizeID { get; set; }
        public int Quantity { get; set; }
        public bool Selected { get; set; }
        public int StockID { get; set; }
    }
}
