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
        UserAccount GetWithCreadentials(UserCredentials userCredentials);
        IEnumerable<UserAccount> GetAllWithOptions(PageModel pageModel);
        int GetCountWithOptions(PageModel pageModel);
        int DeleteWithIds(Guid[] ids);
        int Save(UserAccount userAccount);
    }
}
