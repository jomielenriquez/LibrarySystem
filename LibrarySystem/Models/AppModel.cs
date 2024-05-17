using LibrarySystem.Data.Entities;
using LibrarySystem.Service;
using LibrarySystem.Web.Model;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace LibrarySystem.Models
{
    public class AppModel
    {
        public object? UserAccounts { get; set; }
        public UserAccount UserAccount { get; set; }
        public int UserAccountsCount { get; set; }

        public object? UserRoles { get; set; }
        public UserRole UserRole { get; set; }
        public int UserRolesCount { get; set; }

        public object? Books { get; set; }
        public BookDatabase BookDatabase { get; set; }
        public int BookDatabaseCount { get; set; }

        public object? BorrowedBooks { get; set; }
        public BorrowedBook BorrowedBook { get; set; }
        public int BorrowedBookCount { get; set; }

        public PageModel? currenPage { get; set; }
        public UserCredentials userCredentials { get; set; }
        public UserAccountSearchModel? UserAccountSearch { get; set; }
        public UserRoleSearchModel? UserRoleSearchModel { get; set; }
        public BookDatabaseSearchModel? BookDatabaseSearchModel { get; set; }
        public BorrowedBooksSearchModel? BorrowedBooksSearchModel { get; set; }
        public List<AlertModel> Alerts { get; set; }
        public string? Search { get; set; }
        public Guid? UserAccountID { get; set; }
        public UserAccount CurrentUser { get; set; }
    }
}
