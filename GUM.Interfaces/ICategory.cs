using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUM.ViewModels;

namespace GUM.Interfaces
{
    public interface ICategory
    {
        void Add(Category category);
        List<Category> GetAll();
        Category GetByID(long categoryID);
        bool Delete(int categoryID);
        void Update(ViewModels.Category category);

    }
}
