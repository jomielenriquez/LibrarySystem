using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Entities
{
    public class UserRole
    {
        [Key]
        [Required]
        public Guid UserRoleID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public string? Description { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }

        public ICollection<UserAccount> UserAccounts { get; set; }
    }
}
