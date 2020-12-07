using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUM.ViewModels;

namespace GUM.Interfaces
{
    public interface IProduct
    {
        void Add(Product product);
        List<Product> GetAll();
        Product GetByID(long productID);
        bool Delete(int productID);
        void Update(Product product);
    }
}
