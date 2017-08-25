using SkiaSharp;
using System;
using System.IO;

namespace PhotoGallery.Models
{
    public class ImageProcessor
    {
        const int Quality = 75;

        public void CreateThumbnails(Stream imageStream, string filePath)
        {
            string dir = Path.Combine(Path.GetDirectoryName(filePath), "thumbnail");
            string displayName = Path.GetFileNameWithoutExtension(filePath);
            string ext = Path.GetExtension(filePath);

            Directory.CreateDirectory(dir);

            var format = GetFormat(filePath);

            using (var image = SKBitmap.Decode(imageStream))
            {
                foreach (ImageType type in Enum.GetValues(typeof(ImageType)))
                {
                    int width = (int)type;
                    int height = (int)Math.Round(width * ((float)image.Height / image.Width));

                    string thumbnailPath = Path.Combine(dir, $"{displayName}-{width}x{height}{ext}");
                    var info = new SKImageInfo(width, height);

                    using (var resized = image.Resize(info, SKBitmapResizeMethod.Lanczos3))
                    using (var thumb = SKImage.FromBitmap(resized))
                    using (var fs = new FileStream(thumbnailPath, FileMode.CreateNew, FileAccess.ReadWrite))
                    {
                        thumb.Encode(format, Quality)
                             .SaveTo(fs);
                    }
                }
            }
        }

        private static SKEncodedImageFormat GetFormat(string fileName)
        {
            string ext = Path.GetExtension(fileName.ToLowerInvariant());

            switch (ext)
            {
                case ".gif":
                    return SKEncodedImageFormat.Gif;
                case ".png":
                    return SKEncodedImageFormat.Png;
                case ".webp":
                    return SKEncodedImageFormat.Webp;
            }

            return SKEncodedImageFormat.Jpeg;
        }
    }
}
