using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Data.SearchModel
{
    public class BookDatabaseSearchModel
    {
        [JsonProperty("Title")]
        public string Title { get; set; }
        [JsonProperty("Author")]
        public string Author { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
    }
}
