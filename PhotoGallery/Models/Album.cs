using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoGallery.Models
{
    public class Album
    {
        public Album(string absolutePath)
        {
            AbsolutePath = absolutePath;
            Name = new DirectoryInfo(AbsolutePath).Name;
            Photos = new List<Photo>();
        }

        public string Name { get; }

        public string UrlName
        {
            get
            {
                return Name.Replace(" ", "%20").ToLowerInvariant();
            }
        }

        public string AbsolutePath { get; }

        public List<Photo> Photos { get; }

        public Photo CoverPhoto
        {
            get
            {
                return Photos?.FirstOrDefault();
            }
        }
    }
}