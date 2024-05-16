using LibrarySystem.Data.Entities;
using LibrarySystem.Models;
using LibrarySystem.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserAccountService _userAccountService;

        public HomeController(
            IUserAccountService userAccountService)
        {
            _userAccountService = userAccountService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Account(PageModel pageModel)
        {
            AppModel appModel = new AppModel();

            appModel.UserAccountSearch = pageModel.Search == null ? new UserAccountSearchModel() : JsonConvert.DeserializeObject<UserAccountSearchModel>(pageModel.Search);

            var objects = new List<object>();
            objects.AddRange(_userAccountService.GetAllWithOptions(pageModel).ToList());

            appModel.UserAccounts = new List<object>();
            appModel.UserAccounts = objects;
            appModel.UserAccountsCount = _userAccountService.GetCountWithOptions(pageModel);
            appModel.currenPage = pageModel;

            return View(appModel);
        }

        [HttpPost]
        public IActionResult AccountSearch(UserAccountSearchModel searchModel)
        {
            var page = new PageModel();
            page.Search = JsonConvert.SerializeObject(searchModel);
            return RedirectToAction("Account", "Home", page);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
