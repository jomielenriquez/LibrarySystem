using LibrarySystem.Data.Data;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data
{
    public class UserRoleRepository : IBaseRepository<UserRole>
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int DeleteWithIds(Guid[] id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> GetAll()
        {
            return _dbContext.UserRole.ToList();
        }

        public IEnumerable<UserRole> GetAllWithOptions(PageModel pageModel)
        {
            throw new NotImplementedException();
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            throw new NotImplementedException();
        }

        public UserRole GetWithId(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Save(UserAccount userAccount)
        {
            throw new NotImplementedException();
        }

        public int Save(UserRole data)
        {
            throw new NotImplementedException();
        }

        public int Update(UserRole data)
        {
            throw new NotImplementedException();
        }
    }
}
