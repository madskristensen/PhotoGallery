using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoGallery.Models
{
    public class AlbumCollection
    {
        private IHostingEnvironment _environment;

        public AlbumCollection(IHostingEnvironment environment)
        {
            _environment = environment;
            Albums = new List<Album>();

            Initialize(environment.WebRootPath);
        }

        public List<Album> Albums { get; private set; }

        private void Initialize(string contentPath)
        {
            var root = Path.Combine(contentPath, "albums");
            if (!Directory.Exists(root))
                return;

            var albumPaths = Directory.EnumerateDirectories(root);

            foreach (string albumPath in albumPaths)
            {
                var album = GetAlbum(albumPath);
                Albums.Add(album);
            }

            Albums = Albums.OrderByDescending(a => File.GetLastWriteTime(a.AbsolutePath)).ToList();
        }

        private Album GetAlbum(string albumPath)
        {
            var album = new Album(albumPath);
            var directory = new DirectoryInfo(albumPath);
            var photos = directory.EnumerateFiles()
                .OrderByDescending(f => f.LastWriteTime)
                .Select(a => new Photo(album, a));

            album.Photos.AddRange(photos);

            return album;
        }
    }
}
