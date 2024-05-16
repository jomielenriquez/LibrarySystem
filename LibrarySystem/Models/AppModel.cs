using LibrarySystem.Data.Entities;
using LibrarySystem.Web.Model;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace LibrarySystem.Models
{
    public class AppModel
    {
        public object? UserAccounts { get; set; }
        public object? UserRole { get; set; }
        public int UserAccountsCount { get; set; }
        public PageModel? currenPage { get; set; }
        public UserCredentials userCredentials { get; set; }
        public UserAccountSearchModel? UserAccountSearch { get; set; }
        public List<AlertModel> Alerts { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
