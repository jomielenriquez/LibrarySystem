using LibrarySystem.Data;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Service
{
    public class UserAccountService : IUserAccountService
    {
        private readonly UserAccountRepository _userAccountRepository;
        public UserAccountService(UserAccountRepository userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public bool DeleteConfirmed(Guid id)
        {
            return _userAccountRepository.DeleteConfirmed(id);
        }

        public IEnumerable<UserAccount> GetAll()
        {
            return this._userAccountRepository.GetAll();
        }

        public IEnumerable<UserAccount> GetAllWithOptions(PageModel pageModel)
        {
            return this._userAccountRepository.GetAllWithOptions(pageModel);
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            return this._userAccountRepository.GetCountWithOptions(pageModel);
        }

        public UserAccount GetWithCreadentials(UserCredentials userCredentials)
        {
            return this._userAccountRepository.GetWithCreadentials(userCredentials);
        }
    }
}
