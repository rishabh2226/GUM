
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GUM.DataModels;
using GUM.Interfaces;

namespace GUM.DAL
{
    public class AccountManager : IAccount
    {
        GetUniqueMerchandiseEntities dbContext = null;
        public AccountManager() 
        {
            dbContext = new GetUniqueMerchandiseEntities();
        }

        public bool Register(ViewModels.User user)
        {
            throw new NotImplementedException();
        }

        public ViewModels.User ValidateUser(ViewModels.User user)
        {
            ViewModels.User obj = null;
            try
            {
                obj = dbContext.Users.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).Select(y => new ViewModels.User() { Email = y.Email, RoleID = y.RoleID, RoleName = y.Role.RoleName, FirstName = y.FirstName, LastName = y.LastName, MiddleName = y.MiddleName, Phone = y.Phone }).FirstOrDefault();
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
