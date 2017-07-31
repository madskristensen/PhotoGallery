using Microsoft.AspNetCore.Razor.TagHelpers;
using PhotoGallery.Models;

namespace PhotoGallery.TagHelpers
{
    public class PagingTagHelper : TagHelper
    {
        public IPaginator Model { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("aria-label", "Pagination");
            output.Attributes.SetAttribute("role", "menubar");

            if (Model.Previous != null)
            {
                output.Content.AppendHtml($"<a href=\"{Model.Previous.Link}\" accesskey=\"p\" title=\"{Model.Previous.Name}\">&lt; Prev</a>");
            }
            else
            {
                output.Content.AppendHtml("<span>&lt; Prev</span>");
            }

            if (Model.Next != null)
            {
                output.Content.AppendHtml($"<a href=\"{Model.Next.Link}\" accesskey=\"p\" title=\"{Model.Next.Name}\">Next &gt;</a>");
            }
            else
            {
                output.Content.AppendHtml("<span>Next &gt;</span>");
            }
        }
    }
}
