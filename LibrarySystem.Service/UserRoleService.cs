using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IBaseRepository<UserRole> _userRoleRepository;
        public UserRoleService(IBaseRepository<UserRole> userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public int DeleteWithIds(Guid[] id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> GetAll()
        {
            return _userRoleRepository.GetAll();
        }

        public IEnumerable<UserRole> GetAllWithOptions(PageModel pageModel)
        {
            throw new NotImplementedException();
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            throw new NotImplementedException();
        }

        public int Save(UserAccount userAccount)
        {
            throw new NotImplementedException();
        }
    }
}
