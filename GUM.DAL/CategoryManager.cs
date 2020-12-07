using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GUM.DataModels;
using GUM.Interfaces;
using GUM.ViewModels;

namespace GUM.DAL
{
    public class CategoryManager : ICategory
    {
        GetUniqueMerchandiseEntities dbContext = null;
        public CategoryManager()
        {
            dbContext = new GetUniqueMerchandiseEntities();
        }
        public void Add(ViewModels.Category category)
        {
            var cate = new DataModels.Category() {
                CategoryName=category.CategoryName,
                Description=category.Description,
                Picture=category.Image
            };
            dbContext.Categories.Add(cate);
            dbContext.SaveChanges();
        }

        public bool Delete(int categoryID)
        {
            var category = dbContext.Categories.Find(categoryID);
            //var category = new DataModels.Category { CategoryID = 1 };
            if (category!=null)
            {
                //found the category with given id
                dbContext.Entry(category).State = EntityState.Deleted;
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                //cannot find the category with given id
                return false;
            }           
        }

        public List<ViewModels.Category> GetAll()
        {
            List <ViewModels.Category> categories= null;
            var cate = dbContext.Categories.ToList<DataModels.Category>();
            if (cate != null)
            {
                categories = new List<ViewModels.Category>();
                categories = (from c in dbContext.Categories
                              select new ViewModels.Category() 
                              { 
                                  CategoryID = c.CategoryID, 
                                  Description = c.Description, 
                                  CategoryName = c.CategoryName,
                                  Image = c.Picture 
                              }).ToList();
                //categories=cate.Where(x => x.CategoryID != 0).Select(x =>
                //     {
                //        categories.Add(new ViewModels.Category() {  CategoryID=x.CategoryID,Description=x.Description,CategoryName=x.CategoryName,Picture=x.Picture });
                //    });
            }           
            return categories;
        }

        public ViewModels.Category GetByID(long categoryID)
        {
            var category = (from cate in dbContext.Categories
                            where cate.CategoryID == categoryID
                            select new ViewModels.Category() 
                            {
                                CategoryID=cate.CategoryID,
                                Description=cate.Description,
                                Image = cate.Picture,
                                CategoryName=cate.CategoryName

                            }).FirstOrDefault();
            return category;
        }

        public void Update(ViewModels.Category category)
        {
            //DataModels.Category cate = new DataModels.Category() 
            //{
            //    CategoryName=category.CategoryName,
            //    CategoryID=category.CategoryID,
            //    Picture=category.Picture,
            //    Description=category.Description
            //};
            var cate = (from c in dbContext.Categories
                        where c.CategoryID == category.CategoryID
                        select c).FirstOrDefault();
            if (cate != null)
            {
                cate.CategoryName = category.CategoryName;
                cate.Description = category.Description;
                if (category.Image != null)
                {
                    cate.Picture = category.Image;
                }             
                dbContext.SaveChanges();
            }
        }

    }
}
