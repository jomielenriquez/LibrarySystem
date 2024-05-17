using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Entities
{
    public class BorrowedBook
    {
        [Key]
        [Required]
        public Guid BorrowedBookID{ get; set; }
        [Required]
        public Guid UserAccountID{ get; set; }
        [Required] 
        public Guid BookDatabaseID{ get; set; }
        public bool IsReturned{ get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }
        public UserAccount UserAccount { get; set; }
        public BookDatabase BookDatabase { get; set; }
    }
}
