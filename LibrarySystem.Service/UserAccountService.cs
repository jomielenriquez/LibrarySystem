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
        private readonly IBaseRepository<UserAccount> _userAccountRepository;
        public UserAccountService(IBaseRepository<UserAccount> userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public IEnumerable<UserAccount> GetAll()
        {
            return this._userAccountRepository.GetAll();
        }

        private string ComputeMd5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
