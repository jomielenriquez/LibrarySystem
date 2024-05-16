using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web
{
    [HtmlTargetElement("Lib:Button")]
    public class LibrarySystemButton : TagHelper
    {
        public enum ButtonTypes
        {
            Primary,
            Secondary,
            Success,
            Info,
            Warning,
            Danger,
            Light,
            Dark,
            Link
        }

        public ButtonTypes Type { get; set; }
        public string Caption { get; set; }
        public bool IsSmall { get; set; }
        public string Icon { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";

            string buttonType = "btn-primary";
            if (Type == ButtonTypes.Secondary) buttonType = "btn-secondary";
            else if (Type == ButtonTypes.Success) buttonType = "btn-success";
            else if (Type == ButtonTypes.Info) buttonType = "btn-info";
            else if (Type == ButtonTypes.Warning) buttonType = "btn-warning";
            else if (Type == ButtonTypes.Danger) buttonType = "btn-danger";
            else if (Type == ButtonTypes.Light) buttonType = "btn-light";
            else if (Type == ButtonTypes.Dark) buttonType = "btn-dark";
            else if (Type == ButtonTypes.Link) buttonType = "btn-link";

            if (IsSmall)
            {
                buttonType += " btn-sm";
            }

            output.Attributes.SetAttribute("class", "btn " + buttonType);
            output.Attributes.SetAttribute("type", "button");
            if (!string.IsNullOrEmpty(Icon))
            {
                output.Content.AppendHtml("<i class=\"bi " + Icon + "\"></i> ");
            }
            output.Content.AppendHtml(Caption);
        }
    }
}
