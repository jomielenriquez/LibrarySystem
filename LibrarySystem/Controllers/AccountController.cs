using LibrarySystem.Data.Entities;
using LibrarySystem.Models;
using LibrarySystem.Service;
using LibrarySystem.Web.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAccountService _userAccountService;

        public AccountController(
            IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        [HttpPost]
        public IActionResult LoginUser(UserCredentials userCredentials)
        {
            var userAccount = _userAccountService.GetWithCreadentials(userCredentials);
            if (userAccount != null)
            {
                HttpContext.Session.SetString("UserAccountID", userAccount.UserAccountID.ToString());
                return RedirectToAction("Account", "Home");
            }
            return RedirectToAction("Login", "Account");
        }
        
        public IActionResult Login(UserCredentials? userCredentials)
        {
            AppModel appModel = new AppModel();
            appModel.userCredentials = userCredentials;
            appModel.Alerts = new List<AlertModel>();


            var userAccount = _userAccountService.GetWithCreadentials(userCredentials);
            if (userAccount != null)
            {
                HttpContext.Session.SetString("UserAccountID", userAccount.UserAccountID.ToString());
                return RedirectToAction("Account", "Home");
            }
            else if(userCredentials.UserName != null || userCredentials.Password != null)
            {
                appModel.Alerts.Add(new AlertModel { Type = AlertTypes.Danger, Message = "Invalid username or password" });
            }
            return View(appModel);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost, ActionName("Delete")]
        public bool DeleteAccount([FromBody] Guid[] selected)
        {
            bool isDone = false;
            foreach (var item in selected)
            {
                _userAccountService.DeleteConfirmed(item);
            }
            return true;
        }
    }
}
