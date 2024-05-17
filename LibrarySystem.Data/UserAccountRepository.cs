using LibrarySystem.Data.Data;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data
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

        public IEnumerable<UserAccount> GetAllWithOptions(PageModel pageModel)
        {
            IQueryable<UserAccount> query = _dbContext.UserAccount.AsQueryable();

            PropertyInfo[] properties = typeof(UserAccount).GetProperties();

            // Build the expression tree for dynamic ordering
            ParameterExpression parameter = Expression.Parameter(typeof(UserAccount), "x");
            Expression orderByExpression = null;

            PropertyInfo property = properties.FirstOrDefault(p => p.Name == pageModel.OrderByProperty);
            if (property != null)
            {
                MemberExpression propertyAccess = Expression.MakeMemberAccess(parameter, property);
                orderByExpression = orderByExpression == null
                    ? (Expression)propertyAccess
                    : Expression.Property(orderByExpression, "ThenBy", propertyAccess);
            }

            // Create the final lambda expression
            if (orderByExpression != null)
            {
                LambdaExpression lambda = Expression.Lambda(orderByExpression, parameter);
                query = (IOrderedQueryable<UserAccount>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        pageModel.IsAscending ? "OrderBy" : "OrderByDescending",
                        new Type[] { typeof(UserAccount), orderByExpression.Type },
                        query.Expression,
                        Expression.Quote(lambda)
                    )
                );
            }

            var userRoleSearch = pageModel.Search == null ? new UserAccountSearchModel() : JsonConvert.DeserializeObject<UserAccountSearchModel>(pageModel.Search);

            query = AddFilter(userRoleSearch, query);

            // Apply pagination
            query = query.Skip((pageModel.Page - 1) * pageModel.PageSize).Take(pageModel.PageSize);

            return query.ToList();
        }

        public IQueryable<UserAccount> AddFilter(UserAccountSearchModel userRoleSearch, IQueryable<UserAccount> query)
        {
            if (userRoleSearch != null)
            {
                if (userRoleSearch.AccountFirstName != null)
                {
                    query = query.Where(u => u.FirstName.Contains(userRoleSearch.AccountFirstName));
                }
                if (userRoleSearch.AccountMiddleName != null)
                {
                    query = query.Where(u => u.MiddleName.Contains(userRoleSearch.AccountMiddleName));
                }
                if (userRoleSearch.AccountLastName != null)
                {
                    query = query.Where(u => u.LastName.Contains(userRoleSearch.AccountLastName));
                }
            }
            return query;
        }

        public UserAccount GetWithCreadentials(UserCredentials userCredentials)
        {
            IQueryable<UserAccount> query = _dbContext.UserAccount.AsQueryable();

            query = query.Where(
                u => u.UserName == userCredentials.UserName && u.PasswordHash == userCredentials.PasswordHash);

            return query.FirstOrDefault();
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            IQueryable<UserAccount> query = _dbContext.UserAccount.AsQueryable();

            var userAccountSearch = pageModel.Search == null ? new UserAccountSearchModel() : JsonConvert.DeserializeObject<UserAccountSearchModel>(pageModel.Search);

            query = AddFilter(userAccountSearch, query);

            return query.Count();
        }

        public int DeleteWithIds(Guid[] ids)
        {
            var entitiesToDelete = _dbContext.UserAccount.Where(e => ids.Contains(e.UserAccountID));

            _dbContext.UserAccount.RemoveRange(entitiesToDelete);

            return _dbContext.SaveChanges();
        }

        public int Save(UserAccount userAccount)
        {
            userAccount.CreateDate = DateTime.UtcNow;
            _dbContext.UserAccount.Add(userAccount);
            return _dbContext.SaveChanges();
        }

        public UserAccount GetWithId(Guid id)
        {
            var userAccount = _dbContext.UserAccount.Find(id);
            userAccount.PasswordHash = string.Empty;
            return userAccount;
        }

        public int Update(UserAccount data)
        {
            var userAccount = _dbContext.UserAccount.Find(data.UserAccountID);
            if (userAccount == null)
            {
                return 0;
            }
            userAccount.FirstName = data.FirstName;
            userAccount.LastName = data.LastName;
            userAccount.MiddleName = data.MiddleName;
            userAccount.UserName = data.UserName;
            userAccount.PasswordHash = data.PasswordHash;
            userAccount.UserRoleID = data.UserRoleID;
            userAccount.UpdateDate = DateTime.UtcNow;

            _dbContext.UserAccount.Update(userAccount);
            return _dbContext.SaveChanges();
        }
    }
}
