using GUM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUM.Interfaces
{
    public interface IAccount
    {
        bool Register(User user);
        User ValidateUser(User user);
    }
}
