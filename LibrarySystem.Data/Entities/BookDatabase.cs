using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Entities
{
    public class BookDatabase
    {
        [Key]
        [Required]
        public Guid BookDatabaseID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
        public ICollection<BorrowedBook> BorrowedBooks { get; set; }
    }
}
