using LibrarySystem.Data.Entities;
using LibrarySystem.Service;
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
        public string? Search { get; set; }
        public Guid? UserAccountID { get; set; }
        //public UserAccount User {
        //    get 
        //    {
        //        var user = new UserAccount();
        //        if (UserAccountID != null)
        //        {
        //            return new UserAccountService().get
        //        }
        //    } 
        //}
    }
}
