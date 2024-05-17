using LibrarySystem.Data.Data;
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
    public class BorrowedBooksRepository : IBaseRepository<BorrowedBook>
    {
        private readonly ApplicationDbContext _dbContext;
        public BorrowedBooksRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int DeleteWithIds(Guid[] ids)
        {
            var entitiesToDelete = _dbContext.BorrowedBook.Where(e => ids.Contains(e.BorrowedBookID));

            _dbContext.BorrowedBook.RemoveRange(entitiesToDelete);

            return _dbContext.SaveChanges();
        }

        public IEnumerable<BorrowedBook> GetAll()
        {
            return _dbContext.BorrowedBook.Include(x => x.BookDatabase).ToList();
        }

        public IEnumerable<BorrowedBook> GetAllWithOptions(PageModel pageModel)
        {
            IQueryable<BorrowedBook> query = _dbContext.BorrowedBook.AsQueryable();

            PropertyInfo[] properties = typeof(BorrowedBook).GetProperties();

            // Build the expression tree for dynamic ordering
            ParameterExpression parameter = Expression.Parameter(typeof(BorrowedBook), "x");
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
                query = (IOrderedQueryable<BorrowedBook>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        pageModel.IsAscending ? "OrderBy" : "OrderByDescending",
                        new Type[] { typeof(BorrowedBook), orderByExpression.Type },
                        query.Expression,
                        Expression.Quote(lambda)
                    )
                );
            }

            var search = pageModel.Search == null ? new BorrowedBooksSearchModel() : JsonConvert.DeserializeObject<BorrowedBooksSearchModel>(pageModel.Search);

            query = AddFilter(search, query);

            // Apply pagination
            query = query.Skip((pageModel.Page - 1) * pageModel.PageSize).Take(pageModel.PageSize);

            return query.Include(x => x.UserAccount).ToList();
        }

        public IEnumerable<Borrows> GetBorrowed(PageModel pageModel)
        {
            IQueryable<BorrowedBook> query = _dbContext.BorrowedBook.AsQueryable();

            PropertyInfo[] properties = typeof(BorrowedBook).GetProperties();

            // Build the expression tree for dynamic ordering
            ParameterExpression parameter = Expression.Parameter(typeof(BorrowedBook), "x");
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
                query = (IOrderedQueryable<BorrowedBook>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        pageModel.IsAscending ? "OrderBy" : "OrderByDescending",
                        new Type[] { typeof(BorrowedBook), orderByExpression.Type },
                        query.Expression,
                        Expression.Quote(lambda)
                    )
                );
            }

            var search = pageModel.Search == null ? new BorrowedBooksSearchModel() : JsonConvert.DeserializeObject<BorrowedBooksSearchModel>(pageModel.Search);

            query = AddFilter(search, query);

            // Apply pagination
            query = query.Skip((pageModel.Page - 1) * pageModel.PageSize).Take(pageModel.PageSize);

            return query.Include(x => x.UserAccount).Include(x => x.UserAccount).Select
                (x => new Borrows()
                {
                    Book = x.BookDatabase.Title,
                    Name = $"{x.UserAccount.FirstName} {x.UserAccount.LastName}"
                });
        }
        public IQueryable<BorrowedBook> AddFilter(BorrowedBooksSearchModel search, IQueryable<BorrowedBook> query)
        {
            if (search != null)
            {
                if (search.UserAccountID != Guid.Empty)
                {
                    query = query.Where(u => u.UserAccountID == search.UserAccountID);
                }
                if (search.BookDatabaseID != Guid.Empty)
                {
                    query = query.Where(u => u.BookDatabaseID == search.BookDatabaseID);
                }
            }
            return query;
        }
        public int GetCountWithOptions(PageModel pageModel)
        {
            IQueryable<BorrowedBook> query = _dbContext.BorrowedBook.AsQueryable();

            var bookSearch = pageModel.Search == null ? new BorrowedBooksSearchModel() : JsonConvert.DeserializeObject<BorrowedBooksSearchModel>(pageModel.Search);

            query = AddFilter(bookSearch, query);

            return query.Count();
        }

        public BorrowedBook GetWithId(Guid id)
        {
            var book = _dbContext.BorrowedBook.Find(id);
            return book;
        }

        public int Save(BorrowedBook data)
        {
            data.CreateDate = DateTime.UtcNow;
            _dbContext.BorrowedBook.Add(data);
            return _dbContext.SaveChanges();
        }

        public int Update(BorrowedBook data)
        {
            var borrow = _dbContext.BorrowedBook.Find(data.BorrowedBookID);
            if (borrow == null)
            {
                return 0;
            }
            borrow.UserAccountID = data.UserAccountID;
            borrow.BookDatabaseID = data.BookDatabaseID;
            borrow.UpdateDate = DateTime.UtcNow;

            _dbContext.BorrowedBook.Update(borrow);
            return _dbContext.SaveChanges();
        }
    }
}
