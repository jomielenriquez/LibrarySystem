using LibrarySystem.Data.Entities;
using LibrarySystem.Data.SearchModel;
using LibrarySystem.Extension;
using LibrarySystem.Models;
using LibrarySystem.Service;
using LibrarySystem.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibrarySystem.Controllers
{
    public class BorrowController : Controller
    {
        public readonly IBorrowedBooksService _borrowedBooksService;
        public readonly IBookDatabaseService _bookDatabaseService;
        public readonly IUserAccountService _userAccountService;

        public BorrowController(
            IBorrowedBooksService borrowedBooksService,
            IBookDatabaseService bookDatabaseService,
            IUserAccountService userAccountService)
        {
            _borrowedBooksService = borrowedBooksService;
            _bookDatabaseService = bookDatabaseService;
            _userAccountService = userAccountService;
        }
        public IActionResult Records(PageModel pageModel)
        {
            AppModel appModel = HttpContext.Session.GetOrCreateAppModel();

            appModel.BorrowedBooksSearchModel = !string.IsNullOrEmpty(pageModel.Search)
                ? JsonConvert.DeserializeObject<BorrowedBooksSearchModel>(pageModel.Search)
                : new BorrowedBooksSearchModel();

            var objects = new List<object>();
            objects.AddRange(_borrowedBooksService.GetBorrowed(pageModel).ToList());

            appModel.BorrowedBooks = new List<object>();
            appModel.BorrowedBooks = objects;
            appModel.BorrowedBookCount = _borrowedBooksService.GetCountWithOptions(pageModel);
            appModel.currenPage = pageModel;

            HttpContext.Session.SetObjectAsJson("AppModel", appModel);
            return View(appModel);
        }
        [HttpPost]
        public IActionResult BorrowSearch(BorrowedBooksSearchModel search)
        {
            var pageModel = new PageModel();
            pageModel.Search = JsonConvert.SerializeObject(search);

            return RedirectToAction("Records", "Borrow", pageModel);
        }
        [HttpPost, ActionName("Delete")]
        public int DeleteUserRole([FromBody] Guid[] selected)
        {
            return _borrowedBooksService.DeleteWithIds(selected);
        }
        public IActionResult BorrowEdit(BorrowedBook borrowed)
        {
            AppModel appModel = HttpContext.Session.GetOrCreateAppModel();

            if (borrowed.BookDatabaseID != Guid.Empty)
            {
            }
            appModel.BorrowedBook = borrowed.BookDatabaseID != Guid.Empty
                ? _borrowedBooksService.GetWithId(borrowed.BorrowedBookID) 
                : new BorrowedBook();

            var listOfBooks = new List<object>();
            listOfBooks.AddRange(_bookDatabaseService.GetAll().ToList());

            appModel.Books = new List<object>();
            appModel.Books = listOfBooks;

            var listOfBorrower = new List<object>();
            listOfBorrower.AddRange(_userAccountService.GetAll().ToList());

            appModel.UserAccounts = new List<object>();
            appModel.UserAccounts = listOfBorrower;

            return View(appModel);
        }
        public ActionResult Save(BorrowedBook borrowed)
        {
            var app = new AppModel();
            app.BorrowedBook = new BorrowedBook();

            List<string> required = new List<string>();
            if (borrowed.UserAccountID == Guid.Empty)
            {
                required.Add("Borrower");
            }
            if (borrowed.BookDatabaseID == Guid.Empty)
            {
                required.Add("Book");
            }

            app.Alerts = new List<AlertModel>();
            app.BorrowedBook = borrowed;

            if (required.Count > 0)
            {
                app.Alerts.Add(new AlertModel()
                {
                    Type = AlertTypes.Warning,
                    Message = $"The following fields are required: {string.Join(",", required)}"
                });

                HttpContext.Session.SetObjectAsJson("AppModel", app);
                return RedirectToAction("BorrowEdit", "Borrow", app);
            }

            if (borrowed.BorrowedBookID == Guid.Empty)
            {
                _borrowedBooksService.Save(borrowed);
            }
            else
            {
                _borrowedBooksService.Update(borrowed);
            }

            HttpContext.Session.SetObjectAsJson("AppModel", new AppModel { UserAccountSearch = new UserAccountSearchModel() });
            return RedirectToAction("Records", "Borrow");
        }
    }
}
