using GUM.DataModels;
using GUM.Interfaces;
using GUM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM = GUM.ViewModels;
using DM = GUM.DataModels;

namespace GUM.DAL
{
    public class SizeManager : ISize
    {
        GetUniqueMerchandiseEntities dbContext = null;
        public SizeManager()
        {
            dbContext = new GetUniqueMerchandiseEntities();
        }
        public List<VM.Size> GetAll()
        {
            List<VM.Size> sizes = null;
            var sizeList = dbContext.Sizes.ToList<DM.Size>();
            if (sizeList != null)
            {
                sizes = new List<VM.Size>();
                sizes = (from s in sizeList
                         select new VM.Size()
                              {
                                    SizeID=s.SizeID,
                                    Name=s.Name,
                                    Description=s.Description                                 
                              }).ToList();
            }
            return sizes;
        }
    }
}
