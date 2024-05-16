using LibrarySystem.Data.Entities;
using LibrarySystem.Models;
using LibrarySystem.Service;
using LibrarySystem.Web.Model;
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
            AppModel appModel = new AppModel();
            //appModel.Alerts = new List<AlertModel>();
            return View(appModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
