using LibrarySystem.Data.Entities;
using LibrarySystem.Models;
using LibrarySystem.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserAccountService _userAccountService;

        public HomeController(
            ILogger<HomeController> logger,
            IUserAccountService userAccountService)
        {
            _logger = logger;
            _userAccountService = userAccountService;
        }

        public IActionResult Index(PageModel pageModel)
        {
            var test = _userAccountService.GetAll();

            AppModel appModel = new AppModel();

            var objects = new List<object>();
            objects.AddRange(_userAccountService.GetAll().ToList());

            appModel.UserAccounts = new List<object>();
            appModel.UserAccounts = objects;
            appModel.UserAccountsCount = 1;
            appModel.currenPage = pageModel;

            return View(appModel);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
