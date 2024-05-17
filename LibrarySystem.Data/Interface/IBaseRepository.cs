using LibrarySystem.Data.Entities;
using LibrarySystem.Data.SearchModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Interface
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithOptions(PageModel pageModel);
        int GetCountWithOptions(PageModel pageModel);
        int DeleteWithIds(Guid[] id);
        int Save(T data);
        T GetWithId(Guid id);
        int Update(T data);
    }
}
