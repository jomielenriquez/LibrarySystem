using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web
{
    [HtmlTargetElement("Lib:DropDown")]
    public class LibrarySystemDropdown : TagHelper
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public required List<object> Items { get; set; }
        public required string Value { get; set; }
        public required string Name { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            output.Content.AppendHtml("<label for=\""
                + Id 
                +"\" class=\"form-label\">"
                + Label
                + "</label>");

            output.Content.AppendHtml($"<select class=\"form-select\" id=\"{Id}\" name=\"{Id}\">");

            foreach (var item in Items)
            {
                var value = item.GetType().GetProperty(Value)?.GetValue(item, null)?.ToString();
                var name = item.GetType().GetProperty(Name)?.GetValue(item, null)?.ToString();
                output.Content.AppendHtml($"<option value=\"{value}\">{name}</option>");
            }
            
            output.Content.AppendHtml($"</select>");
        }
    }
}
