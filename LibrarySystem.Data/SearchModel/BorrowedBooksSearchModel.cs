using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.SearchModel
{
    public class BorrowedBooksSearchModel
    {
        [JsonProperty("UserAccountID")]
        public Guid UserAccountID { get; set; }
        [JsonProperty("BookDatabaseID")]
        public Guid BookDatabaseID { get; set; }
    }
}
