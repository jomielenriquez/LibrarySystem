using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web
{
    [HtmlTargetElement("Lib:Form")]
    public class LibrarySystemForm : TagHelper
    {
        public required string Controller { get; set; }
        public required string Action { get; set; }
        public string Id { get; set; }
        public string? SubmitTag { get; set; }
        public string? SubmitIcon { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "SysCoreSearchFormDivClass");

            output.Content.AppendHtml("<form id=\"" + Id + "\" method='post' action='/"
                + Controller
                + "/"
                + Action + "'>");

            output.Content.AppendHtml("<fieldset>");

            output.Content.AppendHtml(output.GetChildContentAsync().Result);

            output.Content.AppendHtml("<button type=\"submit\" class=\"btn btn-primary mt-2 mb-2\"><i class=\"bi " + (SubmitIcon ?? "bi-search") + "\"></i> " + SubmitTag ?? "Submit" + "</button>");
            output.Content.AppendHtml("</fieldset></form>");
        }
    }
}
