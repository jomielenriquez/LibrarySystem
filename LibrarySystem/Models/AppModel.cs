using LibrarySystem.Data.Entities;

namespace LibrarySystem.Models
{
    public class AppModel
    {
        public object? UserAccounts { get; set; }
        public int UserAccountsCount { get; set; }
        public PageModel? currenPage { get; set; }
        public UserCredentials userCredentials { get; set; }
        public UserAccountSearchModel? UserAccountSearch { get; set; }
        public List<string> Errors { get; set; }
    }
}
