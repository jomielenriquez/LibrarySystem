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
    }
}
