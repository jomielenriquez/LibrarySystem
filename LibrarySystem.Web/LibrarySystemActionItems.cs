using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web
{
    [HtmlTargetElement("Lib:ActionItems")]
    public class LibrarySystemActionItems : TagHelper
    {
        public enum TemplateType
        {
            None,
            Default
        }
        public required string Caption { get; set; }
        public TemplateType Template { get; set; }
        public string Id { get; set; }
        public string Table { get; set; }
        public required string Controller { get; set; }
        public string DeleteAction { get; set; }
        public string EditAction { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.Add("style", "margin-bottom: 10px;");

            //Header part
            output.Content.AppendHtml("<div class=\"page-header\"><div class=\"row\"><div class=\"col-lg-12\"><h3>" + this.Caption + "</h3>");
            output.Content.AppendHtml("</div></div></div>");



            output.Content.AppendHtml("<div>");
            // buttons
            if (this.Template == TemplateType.Default)
            {
                output.Content.AppendHtml($"<a href=\"/{Controller}/{EditAction}\" type=\"button\" class=\"btn btn-link btn-sm\"><i class=\"bi bi-plus-lg\"></i> Add</a>");
                output.Content.AppendHtml("<button id=\"" + Id + "Delete\" type=\"button\" class=\"btn btn-link btn-sm\"><i class=\"bi bi-x-circle\"></i> Delete</button>");
            }

            output.Content.AppendHtml(output.GetChildContentAsync().Result);
            output.Content.AppendHtml("</div>");

            // Include JavaScript file
            output.Content.AppendHtml("<script src=\"/js/LibrarySystemActionItems.js\"></script>");
            output.Content.AppendHtml("<script>");
            output.Content.AppendHtml("$(function(){");
            output.Content.AppendHtml("const " + Id + "JsActionItems = new SysCoreActionItems('" + Id + "','" + Table + "','" + Controller + "','" + DeleteAction + "');");
            output.Content.AppendHtml("})");
            output.Content.AppendHtml("</script>");
        }
    }
}
