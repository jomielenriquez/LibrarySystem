using LibrarySystem.Data.Entities;
using LibrarySystem.Data.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Service
{
    public interface IBorrowedBooksService
    {
        IEnumerable<BorrowedBook> GetAll();
        IEnumerable<BorrowedBook> GetAllWithOptions(PageModel pageModel);
        int GetCountWithOptions(PageModel pageModel);
        int DeleteWithIds(Guid[] id);
        int Save(BorrowedBook data);
        BorrowedBook GetWithId(Guid id);
        int Update(BorrowedBook data);
        IEnumerable<Borrows> GetBorrowed(PageModel pageModel);
    }
}
