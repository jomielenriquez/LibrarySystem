using LibrarySystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Service
{
    public interface IUserRoleService
    {
        IEnumerable<UserRole> GetAll();
        IEnumerable<UserRole> GetAllWithOptions(PageModel pageModel);
        int GetCountWithOptions(PageModel pageModel);
        int DeleteWithIds(Guid[] id);
        int Save(UserRole userAccount);
        UserRole GetWithId(Guid id);
        int Update(UserRole data);
    }
}
