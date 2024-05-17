using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.SearchModel
{
    public class Borrows
    {
        public Guid BorrowedBookID { get; set; }
        public string Name { get; set; }
        public string Book { get; set; }
    }
}
