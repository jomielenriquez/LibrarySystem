using LibrarySystem.Data;
using LibrarySystem.Data.Entities;
using LibrarySystem.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Service
{
    public class BookDatabaseService : IBookDatabaseService
    {
        private readonly IBaseRepository<BookDatabase> _bookDatabaseRepository;
        public BookDatabaseService(IBaseRepository<BookDatabase> bookDatabaseRepository)
        {
            _bookDatabaseRepository = bookDatabaseRepository;
        }
        public int DeleteWithIds(Guid[] id)
        {
            return _bookDatabaseRepository.DeleteWithIds(id);
        }

        public IEnumerable<BookDatabase> GetAll()
        {
            return _bookDatabaseRepository.GetAll();
        }

        public IEnumerable<BookDatabase> GetAllWithOptions(PageModel pageModel)
        {
            return _bookDatabaseRepository.GetAllWithOptions(pageModel);
        }

        public int GetCountWithOptions(PageModel pageModel)
        {
            return _bookDatabaseRepository.GetCountWithOptions(pageModel);
        }

        public BookDatabase GetWithId(Guid id)
        {
            return _bookDatabaseRepository.GetWithId(id);
        }

        public int Save(BookDatabase data)
        {
            return _bookDatabaseRepository.Save(data);
        }

        public int Update(BookDatabase data)
        {
            return _bookDatabaseRepository.Update(data);
        }
    }
}
