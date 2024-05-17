using LibrarySystem.Data;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Interface;
using LibrarySystem.Data.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Service
{
    public class BorrowedBooksService : IBorrowedBooksService
    {
        private readonly BorrowedBooksRepository _borrowedBookRepository;
        public BorrowedBooksService(BorrowedBooksRepository borrowedBookRepository)
        {
            _borrowedBookRepository = borrowedBookRepository;
        }
        public int DeleteWithIds(Guid[] id)
        {
            return _borrowedBookRepository.DeleteWithIds(id);
        }

        public IEnumerable<BorrowedBook> GetAll()
        {
            return _borrowedBookRepository.GetAll();
        }

        public IEnumerable<BorrowedBook> GetAllWithOptions(PageModel pageModel)
        {
            return _borrowedBookRepository.GetAllWithOptions(pageModel);
        }

        public IEnumerable<Borrows> GetBorrowed(PageModel pageModel)
        {
            return _borrowedBookRepository.GetBorrowed(pageModel);
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            return _borrowedBookRepository.GetCountWithOptions(pageModel);
        }

        public BorrowedBook GetWithId(Guid id)
        {
            return _borrowedBookRepository.GetWithId(id);
        }

        public int Save(BorrowedBook data)
        {
            return (_borrowedBookRepository.Save(data));
        }

        public int Update(BorrowedBook data)
        {
            return _borrowedBookRepository.Update(data);
        }
    }
}
