using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PhotoGallery.Models;
using System.Collections.Generic;

namespace PhotoGallery.TagHelpers
{
    [HtmlTargetElement("img", TagStructure = TagStructure.WithoutEndTag)]
    public class PhotoTagHelper : TagHelper
    {
        public Photo Photo { get; set; }
        public ImageType Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string thumbnail = Photo.GetThumbnailLink((int)Type, out int height);

            output.Attributes.Add("width", (int)Type);
            output.Attributes.Add("height", height);
            output.Attributes.Add("alt", Photo.DisplayName);
            output.Attributes.Add("src", thumbnail);

            // This is for lazy loading in album view
            if (Type != ImageType.Full)
            {
                output.Attributes.Add("srcset", "/img/_.gif");
                output.Attributes.Add("data-srcset", $"{thumbnail} {(int)Type}vw");
            }
        }
    }

}
