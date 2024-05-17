using LibrarySystem.Data.Data;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Interface;
using LibrarySystem.Data.SearchModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

        public int DeleteWithIds(Guid[] ids)
        {
            var entitiesToDelete = _dbContext.UserRole.Where(e => ids.Contains(e.UserRoleID));

            _dbContext.UserRole.RemoveRange(entitiesToDelete);

            return _dbContext.SaveChanges();
        }

        public IEnumerable<UserRole> GetAll()
        {
            return _dbContext.UserRole.ToList();
        }

        public IEnumerable<UserRole> GetAllWithOptions(PageModel pageModel)
        {
            IQueryable<UserRole> query = _dbContext.UserRole.AsQueryable();

            PropertyInfo[] properties = typeof(UserRole).GetProperties();

            // Build the expression tree for dynamic ordering
            ParameterExpression parameter = Expression.Parameter(typeof(UserRole), "x");
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
                query = (IOrderedQueryable<UserRole>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        pageModel.IsAscending ? "OrderBy" : "OrderByDescending",
                        new Type[] { typeof(UserRole), orderByExpression.Type },
                        query.Expression,
                        Expression.Quote(lambda)
                    )
                );
            }

            var userRoleSearch = pageModel.Search == null ? new UserRoleSearchModel() : JsonConvert.DeserializeObject<UserRoleSearchModel>(pageModel.Search);

            query = AddFilter(userRoleSearch, query);

            // Apply pagination
            query = query.Skip((pageModel.Page - 1) * pageModel.PageSize).Take(pageModel.PageSize);

            return query.ToList();
        }

        public IQueryable<UserRole> AddFilter(UserRoleSearchModel userRoleSearch, IQueryable<UserRole> query)
        {
            if (userRoleSearch != null)
            {
                if (userRoleSearch.UserRoleName != null)
                {
                    query = query.Where(u => u.Name.Contains(userRoleSearch.UserRoleName));
                }
            }
            return query;
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            IQueryable<UserRole> query = _dbContext.UserRole.AsQueryable();

            var userRoleSearch = pageModel.Search == null ? new UserRoleSearchModel() : JsonConvert.DeserializeObject<UserRoleSearchModel>(pageModel.Search);

            query = AddFilter(userRoleSearch, query);

            return query.Count();
        }

        public UserRole GetWithId(Guid id)
        {
            var userRole = _dbContext.UserRole.Find(id);
            return userRole;
        }

        public int Save(UserRole data)
        {
            data.CreateDate = DateTime.UtcNow;
            _dbContext.UserRole.Add(data);
            return _dbContext.SaveChanges();
        }

        public int Update(UserRole data)
        {
            var userRole = _dbContext.UserRole.Find(data.UserRoleID);
            if (userRole == null)
            {
                return 0;
            }
            userRole.Name = data.Name;
            userRole.Code = data.Code;
            userRole.Description = data.Description;
            userRole.UserRoleID = data.UserRoleID;
            userRole.UpdateDate = DateTime.UtcNow;

            _dbContext.UserRole.Update(userRole);
            return _dbContext.SaveChanges();
        }
    }
}
