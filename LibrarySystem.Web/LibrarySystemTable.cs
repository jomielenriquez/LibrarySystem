﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Web
{
    [HtmlTargetElement("Lib:Table")]
    public class LibrarySystemTable : TagHelper
    {
        public enum SelectionModeType
        {
            Single,
            Multiple,
            None
        }
        /// Property to receive the list of data
        public required List<object> Items { get; set; }
        public required object Page { get; set; }
        public int Count { get; set; }

        public SelectionModeType SelectionMode { get; set; }

        private readonly int[] entries = [5, 10, 15, 20];
        public required string Id { get; set; }
        public string Property { get; set; }
        public required string Route { get; set; }
        public string Url { get; set; }
        public int Column { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.TagMode = TagMode.StartTagAndEndTag;

            // Add CSS class for styling
            output.Attributes.Add("class", "table table-hover");
            output.Attributes.Add("id", Id);

            var columns = GetCustomColumnProperties(output.GetChildContentAsync().Result);

            // Generate table header
            output.Content.AppendHtml("<thead><tr>");

            if (SelectionMode == SelectionModeType.Single)
            {
                output.Content.AppendHtml("<th></th>");
            }
            else if (SelectionMode == SelectionModeType.Multiple)
            {
                output.Content.AppendHtml("<th><input class=\"form-check-input\" type=\"checkbox\" name=\"optionsRadios\" id=\"" + Id + "HeaderCheckbox\"></th>");
            }

            string? orderBy = (string?)(Page.GetType().GetProperty("OrderByProperty")?.GetValue(Page));
            bool isAscending = (bool)(Page.GetType().GetProperty("IsAscending")?.GetValue(Page));

            int page = Convert.ToInt32(Page.GetType().GetProperty("Page")?.GetValue(Page));
            int pageSize = Convert.ToInt32(Page.GetType().GetProperty("PageSize")?.GetValue(Page));

            int numberOfPage = Count / pageSize + (Count % pageSize == 0 ? 0 : 1);

            foreach (var column in columns)
            {
                output.Content.AppendHtml($"<th style='cursor:pointer' onclick=\"window.location = '{Route}/?Page={Page}&PageSize={pageSize}&OrderByProperty={column.Prop}&IsAscending={(column.Prop == orderBy && isAscending ? false : true)}'\">{column.Caption} {(orderBy == column.Prop ? "<i class=\"bi bi-sort-" + (isAscending ? "up" : "down") + "\"></i>" : "")}</th>");
            }
            output.Content.AppendHtml("</tr></thead>");

            // Generate table body based on the provided data list and columns
            output.Content.AppendHtml("<tbody>");

            if (Items.Count == 0)
            {
                output.Content.AppendHtml("<tr><td colspan=\"" + columns.Count + "\">No Result<td></tr>");
            }

            foreach (var item in Items)
            {
                output.Content.AppendHtml("<tr>");
                var value = item.GetType().GetProperty(Property)?.GetValue(item, null)?.ToString();
                if (SelectionMode == SelectionModeType.Single)
                {
                    output.Content.AppendHtml($"<td><input value=\"{value}\" class=\"form-check-input\" type=\"radio\" name=\"optionsRadios\"></td>");
                }
                else if (SelectionMode == SelectionModeType.Multiple)
                {
                    output.Content.AppendHtml($"<td><input value=\"{value}\" class=\"form-check-input " + Id + "Class\" type=\"checkbox\" name=\"optionsRadios\"></th>");
                }
                bool isFirst = true;
                foreach (var column in columns)
                {
                    var text = item.GetType().GetProperty(column.Prop)?.GetValue(item, null)?.ToString();
                    if (!string.IsNullOrEmpty(Url) && isFirst)
                    {
                        text = $"<a href=\"{Url}{value}\">{text}</a>";
                        isFirst = false;
                    }
                    output.Content.AppendHtml($"<td>{text}</td>");
                }
                output.Content.AppendHtml("</tr>");
            }
            output.Content.AppendHtml("</tbody>");

            string dropdownHtmlElement = "<div><div class='SysCorePaginationDisplayInline'><p>Show</p></div><div class='SysCorePaginationDisplayInline'><select class=\"form-select SysCorePaginationDropdown\" id=\"" + Id + "Dropdown\">";
            foreach (var entry in entries)
            {
                dropdownHtmlElement += "<option " + (entry == pageSize ? " selected=\"selected\" " : "") + " value=\"" + entry + "\">" + entry + "</option>";
            }
            dropdownHtmlElement += "</select></div><div class='SysCorePaginationDisplayInline'><p>entries</p></div></div>";
            output.PreElement.AppendHtml(dropdownHtmlElement);

            string pagination = "<div class=\"SysCorePaginationDiv\"><p class=\"SysCorePaginationStatus\"></p>  <ul class=\"pagination SysCorePagination\">\r\n    <li class=\"page-item " + (page == 1 ? " disabled" : "") + "\">\r\n      <a class=\"page-link\" href=\""+ Route + "/?Page=" + (page - 1) + "&PageSize=" + pageSize + "&OrderByProperty=" + orderBy + "&IsAscending=" + isAscending + "\">&laquo;</a>\r\n    </li>";

            for (int i = 1; i <= numberOfPage; i++)
            {
                pagination += "<li class=\"page-item " + (i == page ? "active" : "") + " \">\r\n      <a class=\"page-link\" href=\""+ Route + "/?Page=" + i + "&PageSize=" + pageSize + "&OrderByProperty=" + orderBy + "&IsAscending=" + isAscending + "\">" + i + "</a>\r\n    </li>";
            }

            pagination += "<li class=\"page-item " + (page == numberOfPage ? " disabled" : "") + "\">\r\n      <a class=\"page-link\" href=\""+ Route + "/?Page=" + (page + 1) + "&PageSize=" + pageSize + "&OrderByProperty=" + orderBy + "&IsAscending=" + isAscending + "\">&raquo;</a>\r\n    </li>\r\n  </ul>\r\n</div>";

            output.PostElement.AppendHtml(pagination);

            // Include JavaScript file
            output.PostElement.AppendHtml("<script src=\"/js/LibrarySystemTable.js\"></script>");
            output.PreElement.AppendHtml("<script>");
            output.PreElement.AppendHtml("$(function(){");
            output.PreElement.AppendHtml("const " + Id + "JsTable = new MyTable('" + Id + "','" + Route + "');");
            output.PreElement.AppendHtml("})");
            output.PreElement.AppendHtml("</script>");
        }

        // Helper method to get property names of custom-column child tags
        private List<HeaderDetail> GetCustomColumnProperties(TagHelperContent content)
        {
            var columns = new List<string>();

            var propColumns = new List<HeaderDetail>();

            var contentString = content.GetContent();
            var startIndex = 0;

            while (true)
            {
                var customColumnIndex = contentString.IndexOf("<Lib:Column", startIndex);
                if (customColumnIndex == -1)
                    break;

                var propertyIndex = contentString.IndexOf("property=\"", customColumnIndex) + "property=\"".Length;
                var propertyEndIndex = contentString.IndexOf('"', propertyIndex);

                var captionIndex = contentString.IndexOf("caption=\"", customColumnIndex) + "caption=\"".Length;
                var captionEndIndex = contentString.IndexOf('"', captionIndex);

                if (propertyIndex >= 0 && propertyEndIndex > propertyIndex)
                {
                    var property = contentString.Substring(propertyIndex, propertyEndIndex - propertyIndex);
                    var caption = contentString.Substring(captionIndex, captionEndIndex - captionIndex);
                    columns.Add(property);

                    propColumns.Add(new HeaderDetail
                    {
                        Prop = property,
                        Caption = caption,
                    });
                }

                startIndex = propertyEndIndex;
            }

            return propColumns;
        }
        public class HeaderDetail
        {
            public string Prop { get; set; }
            public string Caption { get; set; }
        }
    }
}
