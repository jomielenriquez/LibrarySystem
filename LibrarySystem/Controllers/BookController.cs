using LibrarySystem.Data.Entities;
using LibrarySystem.Extension;
using LibrarySystem.Models;
using LibrarySystem.Service;
using LibrarySystem.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibrarySystem.Controllers
{
    public class BookController : Controller
    {
        public readonly IBookDatabaseService _bookDatabaseService;

        public BookController(IBookDatabaseService bookDatabaseService)
        {
            _bookDatabaseService = bookDatabaseService;
        }
        public IActionResult Books(PageModel pageModel)
        {
            AppModel appModel = HttpContext.Session.GetOrCreateAppModel();
            
            appModel.BookDatabaseSearchModel = !string.IsNullOrEmpty(pageModel.Search)
                ? JsonConvert.DeserializeObject<BookDatabaseSearchModel>(pageModel.Search)
                : new BookDatabaseSearchModel();

            var objects = new List<object>();
            objects.AddRange(_bookDatabaseService.GetAllWithOptions(pageModel).ToList());

            appModel.Books = new List<object>();
            appModel.Books = objects;
            appModel.BookDatabaseCount = _bookDatabaseService.GetCountWithOptions(pageModel);
            appModel.currenPage = pageModel;

            HttpContext.Session.SetObjectAsJson("AppModel", appModel);
            return View(appModel);
        }
        [HttpPost]
        public IActionResult BookSearch(BookDatabaseSearchModel bookSearchModel)
        {
            var pageModel = new PageModel();
            pageModel.Search = JsonConvert.SerializeObject(bookSearchModel);

            return RedirectToAction("Books", "Book", pageModel);
        }
        [HttpPost, ActionName("Delete")]
        public int DeleteUserRole([FromBody] Guid[] selected)
        {
            return _bookDatabaseService.DeleteWithIds(selected);
        }
        public IActionResult BookEdit(BookDatabase bookDatabase)
        {
            AppModel appModel = HttpContext.Session.GetOrCreateAppModel();

            if (bookDatabase.BookDatabaseID != Guid.Empty)
            {
                appModel.BookDatabase = _bookDatabaseService.GetWithId(bookDatabase.BookDatabaseID);
            }

            return View(appModel);
        }
        public ActionResult Save(BookDatabase bookDatabase)
        {
            var app = new AppModel();
            app.BookDatabase = new BookDatabase();

            List<string> required = new List<string>();
            if (string.IsNullOrEmpty(bookDatabase.Title))
            {
                required.Add("Title");
            }
            if (string.IsNullOrEmpty(bookDatabase.Author))
            {
                required.Add("Author");
            }
            if (string.IsNullOrEmpty(bookDatabase.Description))
            {
                required.Add("Description");
            }

            app.Alerts = new List<AlertModel>();
            app.BookDatabase = bookDatabase;

            if (required.Count > 0)
            {
                app.Alerts.Add(new AlertModel()
                {
                    Type = AlertTypes.Warning,
                    Message = $"The following fields are required: {string.Join(",", required)}"
                });

                HttpContext.Session.SetObjectAsJson("AppModel", app);
                return RedirectToAction("BookEdit", "Book", app);
            }

            if (bookDatabase.BookDatabaseID == Guid.Empty)
            {
                _bookDatabaseService.Save(bookDatabase);
            }
            else
            {
                _bookDatabaseService.Update(bookDatabase);
            }

            HttpContext.Session.SetObjectAsJson("AppModel", new AppModel { UserAccountSearch = new UserAccountSearchModel() });
            return RedirectToAction("Books", "Book");
        }
    }
}
