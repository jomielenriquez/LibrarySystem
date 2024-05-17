using LibrarySystem.Data.Entities;
using LibrarySystem.Extension;
using LibrarySystem.Models;
using LibrarySystem.Service;
using LibrarySystem.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibrarySystem.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly IUserAccountService _userAccountService;
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(
            IUserAccountService userAccountService,
            IUserRoleService userRoleService)
        {
            _userAccountService = userAccountService;
            _userRoleService = userRoleService;
        }

        public IActionResult UserRole(PageModel pageModel)
        {
            AppModel appModel = HttpContext.Session.GetOrCreateAppModel();
            if (!string.IsNullOrEmpty(pageModel.Search))
            {
            }
            appModel.UserRoleSearchModel = !string.IsNullOrEmpty(pageModel.Search)
                ? JsonConvert.DeserializeObject<UserRoleSearchModel>(pageModel.Search)
                : new UserRoleSearchModel();

            var objects = new List<object>();
            objects.AddRange(_userRoleService.GetAllWithOptions(pageModel).ToList());

            appModel.UserRoles = new List<object>();
            appModel.UserRoles = objects;
            appModel.UserRolesCount = _userRoleService.GetCountWithOptions(pageModel);
            appModel.currenPage = pageModel;

            HttpContext.Session.SetObjectAsJson("AppModel", appModel);
            return View(appModel);
        }
        [HttpPost]
        public IActionResult UserRoleSearch(UserRoleSearchModel userRoleSearchModel)
        {
            var pageModel = new PageModel();
            pageModel.Search = JsonConvert.SerializeObject(userRoleSearchModel);

            return RedirectToAction("UserRole", "UserRole", pageModel);
        }
        [HttpPost, ActionName("Delete")]
        public int DeleteUserRole([FromBody] Guid[] selected)
        {
            return _userRoleService.DeleteWithIds(selected);
        }
        public IActionResult UserRoleEdit(UserRole userRole)
        {
            AppModel appModel = HttpContext.Session.GetOrCreateAppModel();

            if (userRole.UserRoleID != Guid.Empty)
            {
                appModel.UserRole = _userRoleService.GetWithId(userRole.UserRoleID);
            }

            return View(appModel);
        }
        public ActionResult Save(UserRole userRole)
        {
            var app = new AppModel();
            app.UserRole = new UserRole();

            List<string> required = new List<string>();
            if (string.IsNullOrEmpty(userRole.Name))
            {
                required.Add("Name");
            }
            if (string.IsNullOrEmpty(userRole.Code))
            {
                required.Add("Code");
            }
            if (string.IsNullOrEmpty(userRole.Description))
            {
                required.Add("Description");
            }

            app.Alerts = new List<AlertModel>();
            app.UserRole = userRole;

            if (required.Count > 0)
            {
                app.Alerts.Add(new AlertModel()
                {
                    Type = AlertTypes.Warning,
                    Message = $"The following fields are required: {string.Join(",", required)}"
                });

                HttpContext.Session.SetObjectAsJson("AppModel", app);
                return RedirectToAction("UserRoleEdit", "UserRole", app);
            }

            if (userRole.UserRoleID == Guid.Empty)
            {
                _userRoleService.Save(userRole);
            }
            else
            {
                _userRoleService.Update(userRole);
            }

            HttpContext.Session.SetObjectAsJson("AppModel", new AppModel { UserAccountSearch = new UserAccountSearchModel() });
            return RedirectToAction("UserRole", "UserRole");
        }
    }
}
