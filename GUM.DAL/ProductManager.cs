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
using System.Data.Entity;

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

        public void AddImagesToProduct(VM.Product product)
        {
            foreach (var img in product.Images)
            {
                var productImage = new DM.ProductImage()
                {
                    ProductID = product.ProductID,
                    Image = img
                };
                dbContext.ProductImages.Add(productImage);
            }
            dbContext.SaveChanges();
        }

        public bool Delete(int productID)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProductImage(int productImageID)
        {
            var productImage = dbContext.ProductImages.Find(productImageID);
            if (productImage != null)
            {
                //found the productImage with given id
                dbContext.Entry(productImage).State = EntityState.Deleted;
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                //cannot find the productImage with given id
                return false;
            }
        }

        public List<VM.Product> GetAll()
        {
            List<VM.Product> products =  new List<VM.Product>();
            products = (
                    from p in dbContext.Products
                    select new VM.Product()
                    {
                        SubcategoryID = p.SubCategoryID,
                        SubcategoryName = p.SubCategory.SubCategoryName,
                        CategoryName = p.Category.CategoryName,
                        CategoryID = p.CategoryID,
                        ProductDescription = p.ProductDescription,
                        ProductName = p.ProductName,
                        Color = p.Color,
                        UnitPrice = p.UnitPrice,
                        DiscountPercentage = p.DiscountPercentage,
                        ProductID = p.ProductID,
                        //ProductImages = p.ProductImages.ToList(),
                        //Stocks = p.Stocks.ToList(),
                    }).ToList();

            //var prods = dbContext.Products.ToList<DM.Product>();
            //if (prods != null)
            //{
            //    products = new List<VM.Product>();
            //    products = (from pr in prods
            //                     select new VM.Product()
            //                     {
            //                         SubcategoryID = pr.SubCategoryID,
            //                         SubcategoryName = pr.SubCategory.SubCategoryName,
            //                         CategoryName = pr.Category.CategoryName,
            //                         CategoryID = pr.CategoryID,
            //                         ProductDescription = pr.ProductDescription,                                     
            //                         ProductName = pr.ProductName,
            //                         Color=pr.Color,
            //                         UnitPrice=pr.UnitPrice,
            //                         DiscountPercentage=pr.DiscountPercentage,
            //                         ProductID=pr.ProductID
            //                     }).ToList();
            //    foreach (var p in products)
            //    {
            //        var proImg = dbContext.ProductImages.Where(x => x.ProductID == p.ProductID).ToList();
            //        if (proImg != null)
            //        {
            //            List<VM.ProductImage> productImage = new List<VM.ProductImage>();
            //            productImage = (
            //                             from pr in proImg                                         
            //                             select new VM.ProductImage() 
            //                                { 
            //                                 Image= pr.Image,
            //                                 ProductID=pr.ProductID,
            //                                 ProductImageID=pr.ProductImageID
            //                             }).ToList();
            //            p.ProductImages = productImage;
            //        }

            //        var stocks = dbContext.Stocks.Where(x => x.ProductID == p.ProductID).ToList();
            //        if (stocks != null) 
            //        {
            //            List<VM.Stock> stk = new List<VM.Stock>();
            //            stk = (
            //                    from s in stocks
            //                    select new VM.Stock()
            //                    {
            //                        ProductID = s.ProductID,
            //                        SizeID = s.SizeID,
            //                        Quantity = s.Quantity
            //                    }).ToList();
            //            p.Stocks = stk;
            //        }
            //    }         
            //}
            return products;
        }

        public VM.Product GetByID(long productID)
        {
            throw new NotImplementedException();
        }

        public List<VM.ProductImage> GetImagesUsingProductID(long productID)
        {
            var images = dbContext.ProductImages.Where(x => x.ProductID == productID).Select(x => new VM.ProductImage() { Image=x.Image,ProductID=x.ProductID,ProductImageID=x.ProductImageID}).ToList();
            if (images != null)
                return images;
            else
                return null;
        }

        public VM.ProductImage GetProductImageByID(long productImageID)
        {
            var productImage = dbContext.ProductImages.Where(x => x.ProductImageID == productImageID).Select(x => new VM.ProductImage() { Image = x.Image,ProductID=x.ProductID,ProductImageID=x.ProductImageID}).FirstOrDefault();           
            return productImage;
        }

        public List<VM.Stock> GetStocks(long productID)
        {
            var stocks = dbContext.Stocks.Where(x => x.ProductID == productID).Select(x => new VM.Stock()
            {
                Quantity = x.Quantity,
                SizeID = x.SizeID,
                ProductID = x.ProductID,
                StockID=x.StockID,
                Selected = true
            }).ToList();
            return stocks;
        }

        public void Update(VM.Product product)
        {
            throw new NotImplementedException();
        }

        public bool UpdateStock(VM.Product product)
        {
            int row = 0;
            var dbStocks = dbContext.Stocks.Where(x => x.ProductID == product.ProductID).ToList();
            var sizeIDs = dbStocks.Select(x=>x.SizeID).ToArray();
            var stockIDs = dbStocks.Select(x=>x.StockID).ToArray();
            var stkIDs = product.Stocks.Select(x => x.StockID).ToArray();

            foreach (var st in stockIDs)
                {
                    if (!stkIDs.Contains(st))
                    {
                        var d = dbContext.Stocks.Find(st);
                        dbContext.Stocks.Remove(d);
                        //dbContext.Entry(d).State = EntityState.Deleted;
                        if (dbContext.SaveChanges() > 0)
                        {
                            row++;
                        }
                    }
                }
            foreach (var s in product.Stocks)
                {

                    if (s.StockID > 0)
                    {
                        var stk = dbContext.Stocks.Find(s.StockID);
                        stk.Quantity = s.Quantity;
                        if (dbContext.SaveChanges() > 0)
                        {
                            row++;
                        }

                    }
                    else if (s.StockID == 0 && !sizeIDs.Contains(s.SizeID))
                    {
                        var stock = new DM.Stock()
                        {
                            SizeID = s.SizeID,
                            ProductID = product.ProductID,
                            Quantity = s.Quantity
                        };
                        dbContext.Stocks.Add(stock);
                        if (dbContext.SaveChanges() > 0)
                        {
                            row++;
                        }
                    }
                    else if (s.StockID == 0 && sizeIDs.Contains(s.SizeID))
                    {
                        var stk = dbContext.Stocks.Where(x => x.ProductID == product.ProductID && x.SizeID == s.SizeID).FirstOrDefault();
                        stk.Quantity = s.Quantity;
                        if (dbContext.SaveChanges() > 0)
                        {
                            row++;
                        }
                    }

                }
            if (row > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
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
