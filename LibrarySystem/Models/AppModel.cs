using LibrarySystem.Data.Entities;

namespace LibrarySystem.Models
{
    public class AppModel
    {
        public object? UserAccounts { get; set; }
        public int UserAccountsCount { get; set; }
        public PageModel? currenPage { get; set; }
    }
}
