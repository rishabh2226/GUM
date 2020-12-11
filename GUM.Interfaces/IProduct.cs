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
        List<ProductImage> GetImagesUsingProductID(long productID);
        void AddImagesToProduct(Product product);
        bool DeleteProductImage(int productImageID);
        ProductImage GetProductImageByID(long productImageID);

        List<Stock> GetStocks(long productID);
        bool UpdateStock(Product stocks);
    }
}
