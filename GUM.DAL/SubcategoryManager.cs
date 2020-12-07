using GUM.DataModels;
using GUM.Interfaces;
using GUM.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUM.DAL
{
    public class SubcategoryManager : ISubcategory
    {
        GetUniqueMerchandiseEntities dbContext = null;
        public SubcategoryManager()
        {
            dbContext = new GetUniqueMerchandiseEntities();
        }
        public void Add(Subcategory subcategory)
        {
            var subcate = new DataModels.SubCategory()
            {
                CategoryID=subcategory.CategoryID,
                SubCategoryName = subcategory.SubcategoryName,
                Description = subcategory.Description,
                Picture = subcategory.Image
            };
            dbContext.SubCategories.Add(subcate);
            dbContext.SaveChanges();
        }

        public bool Delete(int subcategoryID)
        {
            var subcategory = dbContext.SubCategories.Find(subcategoryID);
            if (subcategory != null)
            {
                //found the subcategory with given id
                dbContext.Entry(subcategory).State = EntityState.Deleted;
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                //cannot find the subcategory with given id
                return false;
            }
        }

        public List<Subcategory> GetAll()
        {
            List<ViewModels.Subcategory> subcategories = null;
            var cate = dbContext.SubCategories.ToList<DataModels.SubCategory>();
            if (cate != null)
            {
                subcategories = new List<ViewModels.Subcategory>();
                subcategories = (from sc in dbContext.SubCategories
                                 select new ViewModels.Subcategory()
                                 {
                                     SubcategoryID = sc.SubCategoryID,
                                     CategoryID = sc.CategoryID,
                                     Description = sc.Description,
                                     SubcategoryName = sc.SubCategoryName,
                                     Image = sc.Picture,
                                     CategoryName = sc.Category.CategoryName
                                 }).ToList();
            }
            return subcategories;
        }

        public Subcategory GetByID(long subcategoryID)
        {
            var subcategory = (from sc in dbContext.SubCategories
                            where sc.SubCategoryID == subcategoryID
                            select new ViewModels.Subcategory()
                            {
                                SubcategoryID = sc.SubCategoryID,
                                CategoryID = sc.CategoryID,
                                Description = sc.Description,
                                SubcategoryName = sc.SubCategoryName,
                                Image = sc.Picture,
                                CategoryName=sc.Category.CategoryName

                            }).FirstOrDefault();
            return subcategory;
        }

        public void Update(Subcategory subcategory)
        {
            var subcate = (from c in dbContext.SubCategories
                        where c.SubCategoryID == subcategory.SubcategoryID
                        select c).FirstOrDefault();
            if (subcate != null)
            {
                subcate.SubCategoryName = subcategory.SubcategoryName;
                subcate.CategoryID = subcategory.CategoryID;
                subcate.Description = subcategory.Description;
                if (subcategory.Image != null)
                {
                    subcate.Picture = subcategory.Image;
                }
                //subcate.Picture = subcategory.Image;
                dbContext.SaveChanges();
            }
        }
    }
}
