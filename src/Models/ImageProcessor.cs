using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace PhotoGallery.Models
{
    public class ImageProcessor
    {
        public void CreateThumbnails(string albumName, string filePath)
        {
            var image = Image.FromFile(filePath);

            try
            {
                string dir = Path.Combine(Path.GetDirectoryName(filePath), "thumbnail");
                string displayName = Path.GetFileNameWithoutExtension(filePath);
                string ext = Path.GetExtension(filePath);

                Directory.CreateDirectory(dir);

                foreach (ImageType type in Enum.GetValues(typeof(ImageType)))
                {
                    int width = (int)type;
                    int height = (int)Math.Round(width * ((float)image.Height / image.Width));

                    string thumbnailPath = Path.Combine(dir, $"{displayName}-{width}x{height}{ext}");

                    using (var thumbnail = ResizeImage(image, width, height))
                    {
                        thumbnail.Save(thumbnailPath, image.RawFormat);
                    }
                }
            }
            finally
            {
                image.Dispose();
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(96, 96);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
