using LibrarySystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Service
{
    public interface IUserAccountService
    {
        IEnumerable<UserAccount> GetAll();
    }
}
