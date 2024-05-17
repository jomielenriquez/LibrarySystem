using LibrarySystem.Data.Entities;
using LibrarySystem.Data.SearchModel;
using LibrarySystem.Extension;
using LibrarySystem.Models;
using LibrarySystem.Service;
using LibrarySystem.Web.Model;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace LibrarySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IUserRoleService _userRoleService;

        public AccountController(
            IUserAccountService userAccountService,
            IUserRoleService userRoleService)
        {
            _userAccountService = userAccountService;
            _userRoleService = userRoleService;
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
                return RedirectToAction("Dashboard", "Home");
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
        public int DeleteAccount([FromBody] Guid[] selected)
        {
            return _userAccountService.DeleteWithIds(selected);
        }
        public IActionResult Account(PageModel pageModel)
        {
            AppModel appModel = HttpContext.Session.GetOrCreateAppModel();
            if (!string.IsNullOrEmpty(pageModel.Search))
            {
                appModel.UserAccountSearch = JsonConvert.DeserializeObject<UserAccountSearchModel>(pageModel.Search);
            }

            var objects = new List<object>();
            objects.AddRange(_userAccountService.GetAllWithOptions(pageModel).ToList());

            appModel.UserAccounts = new List<object>();
            appModel.UserAccounts = objects;
            appModel.UserAccountsCount = _userAccountService.GetCountWithOptions(pageModel);
            appModel.currenPage = pageModel;

            HttpContext.Session.SetObjectAsJson("AppModel", appModel);

            return View(appModel);
        }

        public IActionResult AccountEdit(UserAccount userAccount)
        {
            AppModel appModel = HttpContext.Session.GetOrCreateAppModel();

            if(userAccount.UserAccountID != Guid.Empty)
            {
                appModel.UserAccount = _userAccountService.GetWithId(userAccount.UserAccountID);
            }

            var objects = new List<object>();
            objects.AddRange(_userRoleService.GetAll().ToList());

            appModel.UserRoles = new List<object>();
            appModel.UserAccounts = objects;

            return View(appModel);
        }
        public ActionResult Save(UserAccount userAccount)
        {
            var app = new AppModel();
            app.UserAccount = new UserAccount();

            List<string> required = new List<string>();
            if (string.IsNullOrEmpty(userAccount.UserName))
            {
                required.Add("UserName");
            }
            if (string.IsNullOrEmpty(userAccount.PasswordHash))
            {
                required.Add("Password");
            }
            if (string.IsNullOrEmpty(userAccount.FirstName))
            {
                required.Add("First Name");
            }
            if (string.IsNullOrEmpty(userAccount.LastName))
            {
                required.Add("Last Name");
            }
            if (userAccount.UserRoleID == Guid.Empty)
            {
                required.Add("Role");
            }

            app.Alerts = new List<AlertModel>();
            app.UserAccount = userAccount;

            if (required.Count > 0)
            {
                app.Alerts.Add(new AlertModel()
                {
                    Type = AlertTypes.Warning,
                    Message = $"The following fields are required: {string.Join(",", required)}"
                });

                HttpContext.Session.SetObjectAsJson("AppModel", app);
                return RedirectToAction("AccountEdit", "Account", app);
            }
            
            var userCredential = new UserCredentials
            {
                UserName = userAccount.UserName,
                Password = userAccount.PasswordHash
            };

            userAccount.PasswordHash = userCredential.PasswordHash;

            if(userAccount.UserAccountID == Guid.Empty)
            {
                _userAccountService.Save(userAccount);
            }
            else
            {
                _userAccountService.Update(userAccount);
            }

            HttpContext.Session.SetObjectAsJson("AppModel", new AppModel { UserAccountSearch = new UserAccountSearchModel()});
            return RedirectToAction("Account", "Account");
        }

        [HttpPost]
        public IActionResult AccountSearch(UserAccountSearchModel searchModel)
        {
            var pageModel = new PageModel();
            pageModel.Search = JsonConvert.SerializeObject(searchModel);
            
            return RedirectToAction("Account", "Account", pageModel);
        }
    }
}
