using LibrarySystem.Web.Model;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web
{
    [HtmlTargetElement("Lib:Alert")]
    public class LibrarySystemAlert : TagHelper
    {
        public List<AlertModel> Alerts { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            foreach(var alert in Alerts)
            {
                output.Content.AppendHtml($"<div class=\"alert alert-dismissible alert-{alert.Type.ToString().ToLower()}\"><button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\"></button>{alert.Message}</div>");
            }
        }
    }
}
