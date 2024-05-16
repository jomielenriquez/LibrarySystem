﻿using LibrarySystem.Data.Data;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Interface;
using Microsoft.EntityFrameworkCore;
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
                u => u.UserName == userCredentials.UserName && u.PasswordHash == userCredentials.Password);

            return query.FirstOrDefault();
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            IQueryable<UserAccount> query = _dbContext.UserAccount.AsQueryable();

            var userAccountSearch = pageModel.Search == null ? new UserAccountSearchModel() : JsonConvert.DeserializeObject<UserAccountSearchModel>(pageModel.Search);

            query = AddFilter(userAccountSearch, query);

            return query.Count();
        }

        public bool DeleteConfirmed(Guid id)
        {
            var user = _dbContext.UserAccount.Find(id);
            if(user == null)
            {
                return false;
            }

            _dbContext.UserAccount.Remove(user);
            _dbContext.SaveChangesAsync();

            return true;
        }
    }
}