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
    public class BookDatabaseRepository : IBaseRepository<BookDatabase>
    {
        private readonly ApplicationDbContext _dbContext;
        public BookDatabaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int DeleteWithIds(Guid[] ids)
        {
            var entitiesToDelete = _dbContext.BookDatabase.Where(e => ids.Contains(e.BookDatabaseID));

            _dbContext.BookDatabase.RemoveRange(entitiesToDelete);

            return _dbContext.SaveChanges();
        }

        public IEnumerable<BookDatabase> GetAll()
        {
            return _dbContext.BookDatabase.ToList();
        }

        public IEnumerable<BookDatabase> GetAllWithOptions(PageModel pageModel)
        {
            IQueryable<BookDatabase> query = _dbContext.BookDatabase.AsQueryable();

            PropertyInfo[] properties = typeof(BookDatabase).GetProperties();

            // Build the expression tree for dynamic ordering
            ParameterExpression parameter = Expression.Parameter(typeof(BookDatabase), "x");
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
                query = (IOrderedQueryable<BookDatabase>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        pageModel.IsAscending ? "OrderBy" : "OrderByDescending",
                        new Type[] { typeof(BookDatabase), orderByExpression.Type },
                        query.Expression,
                        Expression.Quote(lambda)
                    )
                );
            }

            var bookSearch = pageModel.Search == null ? new BookDatabaseSearchModel() : JsonConvert.DeserializeObject<BookDatabaseSearchModel>(pageModel.Search);

            query = AddFilter(bookSearch, query);

            // Apply pagination
            query = query.Skip((pageModel.Page - 1) * pageModel.PageSize).Take(pageModel.PageSize);

            return query.ToList();
        }

        public IQueryable<BookDatabase> AddFilter(BookDatabaseSearchModel bookDatabaseSearchModel, IQueryable<BookDatabase> query)
        {
            if (bookDatabaseSearchModel != null)
            {
                if (bookDatabaseSearchModel.Title != null)
                {
                    query = query.Where(u => u.Title.Contains(bookDatabaseSearchModel.Title));
                }
                if (bookDatabaseSearchModel.Author != null)
                {
                    query = query.Where(u => u.Author.Contains(bookDatabaseSearchModel.Author));
                }
                if (bookDatabaseSearchModel.Description != null)
                {
                    query = query.Where(u => u.Description.Contains(bookDatabaseSearchModel.Description));
                }
            }
            return query;
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            IQueryable<BookDatabase> query = _dbContext.BookDatabase.AsQueryable();

            var bookSearch = pageModel.Search == null ? new BookDatabaseSearchModel() : JsonConvert.DeserializeObject<BookDatabaseSearchModel>(pageModel.Search);

            query = AddFilter(bookSearch, query);

            return query.Count();
        }

        public BookDatabase GetWithId(Guid id)
        {
            var book = _dbContext.BookDatabase.Find(id);
            return book;
        }

        public int Save(BookDatabase data)
        {
            data.CreateDate = DateTime.UtcNow;
            _dbContext.BookDatabase.Add(data);
            return _dbContext.SaveChanges();
        }

        public int Update(BookDatabase data)
        {
            var book = _dbContext.BookDatabase.Find(data.BookDatabaseID);
            if (book == null)
            {
                return 0;
            }
            book.Title = data.Title;
            book.Author = data.Author;
            book.Description = data.Description;
            book.Quantity = data.Quantity;
            book.UpdateDate = DateTime.UtcNow;

            _dbContext.BookDatabase.Update(book);
            return _dbContext.SaveChanges();
        }
    }
}
