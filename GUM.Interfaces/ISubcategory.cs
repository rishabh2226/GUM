using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUM.ViewModels;

namespace GUM.Interfaces
{
    public interface ISubcategory
    {
        void Add(Subcategory subcategory);
        List<Subcategory> GetAll();
        Subcategory GetByID(long subcategoryID);
        bool Delete(int subcategoryID);
        void Update(ViewModels.Subcategory subcategory);
    }
}
