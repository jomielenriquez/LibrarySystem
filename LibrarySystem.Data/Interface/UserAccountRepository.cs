using LibrarySystem.Data.Data;
using LibrarySystem.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Interface
{
    public class UserAccountRepository : IBaseRepository<UserAccount>
    {
        private readonly ApplicationDbContext _dbContext;
        public UserAccountRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<UserAccount> GetAll()
        {
            return _dbContext.UserAccount.Include(u => u.UserRole).ToList();
        }
    }
}
