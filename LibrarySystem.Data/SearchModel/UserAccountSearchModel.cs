using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibrarySystem.Data.SearchModel
{
    public class UserAccountSearchModel
    {
        [JsonProperty("FistName")]
        public string AccountFirstName { get; set; }
        [JsonProperty("MiddleName")]
        public string AccountMiddleName { get; set; }
        [JsonProperty("LastName")]
        public string AccountLastName { get; set; }
    }
}
