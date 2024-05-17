using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web.Model
{
    public enum AlertTypes
    {
        Warning,
        Danger,
        Primary,
        Success
    }
    public class AlertModel
    {
        public AlertTypes Type { get; set; }
        public string Message { get; set; }
    }
}
