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
            return _userRoleRepository.DeleteWithIds(id);
        }

        public IEnumerable<UserRole> GetAll()
        {
            return _userRoleRepository.GetAll();
        }

        public IEnumerable<UserRole> GetAllWithOptions(PageModel pageModel)
        {
            return _userRoleRepository.GetAllWithOptions(pageModel);
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            return _userRoleRepository.GetCountWithOptions(pageModel);
        }

        public UserRole GetWithId(Guid id)
        {
            return _userRoleRepository.GetWithId(id);
        }

        public int Save(UserRole userAccount)
        {
            return _userRoleRepository.Save(userAccount);
        }

        public int Update(UserRole data)
        {
            return _userRoleRepository.Update(data);
        }
    }
}
