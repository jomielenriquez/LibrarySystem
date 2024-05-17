using LibrarySystem.Data.Entities;
using LibrarySystem.Data.SearchModel;
using LibrarySystem.Models;
using LibrarySystem.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace LibrarySystem.Extension
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            //session.SetString(key, JsonConvert.SerializeObject(value));
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            session.SetString(key, JsonConvert.SerializeObject(value, settings));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static AppModel GetOrCreateAppModel(this ISession session)
        {
            var appModel = session.GetObjectFromJson<AppModel>("AppModel");
            if (appModel == null)
            {
                appModel = new AppModel
                {
                    UserAccountSearch = new UserAccountSearchModel(),
                    UserRoleSearchModel = new UserRoleSearchModel(),
                    BookDatabaseSearchModel = new BookDatabaseSearchModel(),
                    BorrowedBooksSearchModel = new BorrowedBooksSearchModel(),
                    UserAccount = new UserAccount(),
                    UserRole = new UserRole(),
                    BookDatabase = new BookDatabase(),
                    BorrowedBook = new BorrowedBook(),
                };
                if (!string.IsNullOrEmpty(appModel.Search))
                {
                    appModel.UserAccountSearch = JsonConvert.DeserializeObject<UserAccountSearchModel>(appModel.Search);
                }

                session.SetObjectAsJson("AppModel", appModel);
            }
            return appModel;
        }
    }
}
