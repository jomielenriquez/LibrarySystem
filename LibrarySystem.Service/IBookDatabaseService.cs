using LibrarySystem.Data.Entities;
using LibrarySystem.Data.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Service
{
    public interface IBookDatabaseService
    {
        IEnumerable<BookDatabase> GetAll();
        IEnumerable<BookDatabase> GetAllWithOptions(PageModel pageModel);
        int GetCountWithOptions(PageModel pageModel);
        int DeleteWithIds(Guid[] id);
        int Save(BookDatabase data);
        BookDatabase GetWithId(Guid id);
        int Update(BookDatabase data);
    }
}
