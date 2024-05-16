using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web
{
    [HtmlTargetElement("Lib:Textbox")]
    public class LibrarySystemTextbox :TagHelper
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public string Placeholder { get; set; }
        public string Value { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "SysCoreTextBoxDivClass");

            output.Content.AppendHtml("<label for=\""
                + Id
                + "\" class=\"form-label mt-0 " + Id + "SysCoreTextBoxLabelClass\">"
                + Label + "</label>");

            output.Content.AppendHtml("<input type=\""
                + Type
                + "\" class=\"form-control " + Id
                + "SysCoreTextBoxClass\" id=\""
                + Id
                + "\" name=\"" + Id
                + "\" value=\"" + Value
                + "\" placeholder=\"" + Placeholder + "\">");

        }
    }
}
