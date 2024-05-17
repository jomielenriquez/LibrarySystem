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

        public int DeleteWithIds(Guid[] ids)
        {
            return _userAccountRepository.DeleteWithIds(ids);
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

        public UserAccount GetWithId(Guid id)
        {
            return _userAccountRepository.GetWithId(id);
        }

        public int Save(UserAccount userAccount)
        {
            return this._userAccountRepository.Save(userAccount);
        }

        public int Update(UserAccount data)
        {
            return _userAccountRepository.Update(data);
        }
    }
}
