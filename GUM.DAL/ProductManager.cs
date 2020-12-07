using GUM.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM=GUM.ViewModels;
using DM=GUM.DataModels;
using System.Security.Cryptography;
using GUM.Interfaces;

namespace GUM.DAL
{
    public class ProductManager: IProduct
    {

        GetUniqueMerchandiseEntities dbContext = null;
        public ProductManager()
        {
            dbContext = new GetUniqueMerchandiseEntities();
        }
        public void Add(VM.Product product)
        {
            var prod = new DM.Product()
            {
                CategoryID = product.CategoryID,
                SubCategoryID = product.SubcategoryID,
                ProductDescription = product.ProductDescription,
                Color=product.Color,
                UnitPrice=product.UnitPrice,
                ProductName=product.ProductName,
                DiscountPercentage=product.DiscountPercentage              
            };
            var insertedProduct=dbContext.Products.Add(prod);
            foreach (var s in product.Stocks)
            {
                var stock = new DM.Stock() 
                {
                    SizeID=s.SizeID,
                    ProductID=insertedProduct.ProductID,
                    Quantity=s.Quantity
                };
                dbContext.Stocks.Add(stock);
                //dbContext.SaveChanges();
            }
            foreach (var img in product.Images)
            {
                var productImage = new DM.ProductImage() {
                    ProductID = insertedProduct.ProductID,
                    Image=img
                };
                dbContext.ProductImages.Add(productImage);
                //dbContext.SaveChanges();
            }
            dbContext.SaveChanges();
        }

        public bool Delete(int productID)
        {
            throw new NotImplementedException();
        }

        //public bool Delete(int subcategoryID)
        //{
        //    var subcategory = dbContext.SubCategories.Find(subcategoryID);
        //    if (subcategory != null)
        //    {
        //        //found the subcategory with given id
        //        dbContext.Entry(subcategory).State = EntityState.Deleted;
        //        dbContext.SaveChanges();
        //        return true;
        //    }
        //    else
        //    {
        //        //cannot find the subcategory with given id
        //        return false;
        //    }
        //}

        public List<VM.Product> GetAll()
        {
            List<VM.Product> products = null;
            var prods = dbContext.Products.ToList<DM.Product>();
            if (prods != null)
            {
                products = new List<VM.Product>();
                products = (from pr in prods
                                 select new VM.Product()
                                 {
                                     SubcategoryID = pr.SubCategoryID,
                                     SubcategoryName = pr.SubCategory.SubCategoryName,
                                     CategoryName = pr.Category.CategoryName,
                                     CategoryID = pr.CategoryID,
                                     ProductDescription = pr.ProductDescription,                                     
                                     ProductName = pr.ProductName,
                                     Color=pr.Color,
                                     UnitPrice=pr.UnitPrice,
                                     DiscountPercentage=pr.DiscountPercentage,
                                     ProductID=pr.ProductID
                                 }).ToList();
                foreach (var p in products)
                {
                    var proImg = dbContext.ProductImages.Where(x => x.ProductID == p.ProductID).ToList();
                    if (proImg != null)
                    {
                        List<VM.ProductImage> productImage = new List<VM.ProductImage>();
                        productImage = (
                                         from pr in proImg                                         
                                         select new VM.ProductImage() 
                                            { 
                                             Image= pr.Image,
                                             ProductID=pr.ProductID,
                                             ProductImageID=pr.ProductImageID
                                         }).ToList();
                        p.ProductImages = productImage;
                    }

                    var stocks = dbContext.Stocks.Where(x => x.ProductID == p.ProductID).ToList();
                    if (stocks != null) 
                    {
                        List<VM.Stock> stk = new List<VM.Stock>();
                        stk = (
                                from s in stocks
                                select new VM.Stock()
                                {
                                    ProductID = s.ProductID,
                                    SizeID = s.SizeID,
                                    Quantity = s.Quantity
                                }).ToList();
                        p.Stocks = stk;
                    }
                }         
            }
            return products;
        }

        public VM.Product GetByID(long productID)
        {
            throw new NotImplementedException();
        }

        public void Update(VM.Product product)
        {
            throw new NotImplementedException();
        }

        //public Subcategory GetByID(long subcategoryID)
        //{
        //    var subcategory = (from sc in dbContext.SubCategories
        //                       where sc.SubCategoryID == subcategoryID
        //                       select new ViewModels.Subcategory()
        //                       {
        //                           SubcategoryID = sc.SubCategoryID,
        //                           CategoryID = sc.CategoryID,
        //                           Description = sc.Description,
        //                           SubcategoryName = sc.SubCategoryName,
        //                           Image = sc.Picture,
        //                           CategoryName = sc.Category.CategoryName

        //                       }).FirstOrDefault();
        //    return subcategory;
        //}

        //public void Update(Subcategory subcategory)
        //{
        //    var subcate = (from c in dbContext.SubCategories
        //                   where c.SubCategoryID == subcategory.SubcategoryID
        //                   select c).FirstOrDefault();
        //    if (subcate != null)
        //    {
        //        subcate.SubCategoryName = subcategory.SubcategoryName;
        //        subcate.CategoryID = subcategory.CategoryID;
        //        subcate.Description = subcategory.Description;
        //        if (subcategory.Image != null)
        //        {
        //            subcate.Picture = subcategory.Image;
        //        }
        //        //subcate.Picture = subcategory.Image;
        //        dbContext.SaveChanges();
        //    }
        //}
    }
}
