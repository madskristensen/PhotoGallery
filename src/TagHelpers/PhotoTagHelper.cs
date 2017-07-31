using Microsoft.AspNetCore.Razor.TagHelpers;
using PhotoGallery.Models;

namespace PhotoGallery.TagHelpers
{
    [HtmlTargetElement("img", Attributes = "photo, type")]
    public class PhotoTagHelper : TagHelper
    {
        public Photo Photo { get; set; }
        public ImageType Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            string thumbnail = Photo.GetThumbnailLink((int)Type, out int height);

            if (string.IsNullOrEmpty(thumbnail))
            {
                output.SuppressOutput();
                return;
            }

            output.Attributes.Add("width", (int)Type);
            output.Attributes.Add("height", height);
            output.Attributes.Add("alt", Photo.DisplayName);
            output.Attributes.Add("src", thumbnail);
            output.Attributes.Add("class", "lazy");

            if (Type != ImageType.Full)
            {
                // This is for lazy loading in album view
                output.Attributes.Add("srcset", "/img/_.gif");

                string thumb = Photo.GetThumbnailLink((int)ImageType.Thumbnail, out int thumbHeight);
                string cover = Photo.GetThumbnailLink((int)ImageType.Cover, out int coverHeight);
                output.Attributes.Add("data-srcset", $"{thumb} {(int)ImageType.Thumbnail}w, {cover} {(int)ImageType.Cover}w");
            }
        }
    }

}
