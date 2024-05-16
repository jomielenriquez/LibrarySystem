using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.Entities
{
    public class UserAccount
    {
        [Key]
        [Required]
        public Guid UserAccountID { get; set; }
        public required string UserName { get; set; }
        public required string PasswordHash { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public required Guid UserRoleID { get; set; }
        public required DateTime CreateDate { get; set; }
        public string? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdateBy { get; set; }

        public UserRole UserRole { get; set; }
    }

}
