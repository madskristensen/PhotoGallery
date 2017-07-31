using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhotoGallery.Models
{
    public class Photo : IPaginator
    {
        private Dictionary<int, int> _heights = new Dictionary<int, int>();
        private static Regex _size = new Regex(@"(?<name>.+)-(?<width>[0-9]+)x(?<height>[0-9]+).", RegexOptions.Compiled);

        public Photo(Album album, FileInfo file)
        {
            Album = album;
            AbsolutePath = file.FullName;
            LastModified = file.LastWriteTimeUtc;
        }

        public string Name
        {
            get
            {
                return Path.GetFileName(AbsolutePath);
            }
        }

        public string DisplayName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(Name);
            }
        }

        public string UrlName
        {
            get
            {
                return DisplayName.Replace(" ", "%20").ToLowerInvariant();
            }
        }

        public Album Album { get; }

        public string AbsolutePath { get; set; }

        public DateTime LastModified { get; }

        public IPaginator Next
        {
            get
            {
                int index = Album.Photos.IndexOf(this);

                if (index < Album.Photos.Count - 1)
                {
                    return Album.Photos[index + 1];
                }

                return null;
            }
        }

        public IPaginator Previous
        {
            get
            {
                int index = Album.Photos.IndexOf(this);

                if (index > 0)
                {
                    return Album.Photos[index - 1];
                }

                return null;
            }
        }

        public string Link
        {
            get
            {
                return $"/photo/{Album.UrlName}/{UrlName}/";
            }
        }

        public string DownloadLink
        {
            get
            {
                return $"/albums/{Album.UrlName}/{Name}";
            }
        }

        public string ThumbnailDirectory
        {
            get
            {
                return $"/albums/{Album.UrlName}/thumbnail/";
            }
        }

        public string GetThumbnailLink(int width, out int height)
        {
            string ext = Path.GetExtension(Name);

            if (_heights.TryGetValue(width, out height))
            {
                return GenerateThumbnailLink(width, height, ext);
            }

            string absoluteDir = Path.Combine(Path.GetDirectoryName(AbsolutePath), "thumbnail");
            string pattern = $"{DisplayName}-{width}x*{ext}";
            var thumbnail = Directory.GetFiles(absoluteDir, pattern).FirstOrDefault();

            if (!string.IsNullOrEmpty(thumbnail))
            {
                string fileName = Path.GetFileName(thumbnail);
                Match match = _size.Match(fileName);

                if (match.Success)
                {
                    height = int.Parse(match.Groups["height"].Value);
                    _heights[width] = height;
                    return GenerateThumbnailLink(width, height, ext);
                }
            }

            return null;
        }

        private string GenerateThumbnailLink(int width, int height, string ext)
        {
            return $"{ThumbnailDirectory}{UrlName}-{width}x{height}{ext}";
        }
    }
}