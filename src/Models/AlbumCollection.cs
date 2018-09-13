using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PhotoGallery.Models
{
    public class AlbumCollection
    {
        private IHostingEnvironment _environment;
        private static readonly string[] _extensions = { ".jpg", ".jpeg", ".gif", ".png" };
        private readonly ImageProcessor _imageProcessor;

        public AlbumCollection(IHostingEnvironment environment, ImageProcessor imageProcessor)
        {
            _environment = environment;
            Albums = new List<Album>();
            _imageProcessor = imageProcessor;

            Initialize(environment.WebRootPath);
        }

        public List<Album> Albums { get; private set; }

        public bool IsImageFile(string file)
        {
            string ext = Path.GetExtension(file);
            return _extensions.Contains(ext, StringComparer.OrdinalIgnoreCase);
        }

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

            Sort();
        }

        public void Sort()
        {
            Albums = Albums.OrderBy(a => a.Name).ToList();
        }

        private Album GetAlbum(string albumPath)
        {
            var album = new Album(albumPath, this);
            var directory = new DirectoryInfo(albumPath);
            var photos = directory.EnumerateFiles()
                .Where(f => IsImageFile(f.FullName))
                //.OrderByDescending(f => f.LastWriteTime)
                .Select(a => new Photo(album, a));

            album.Photos.AddRange(photos);
            album.Sort();

            GenerateThumbnails(album);

            return album;
        }

        private void GenerateThumbnails(Album album)
        {
            foreach (var photo in album.Photos)
            {
                if (String.IsNullOrWhiteSpace(photo.GetThumbnailLink((int)ImageType.Thumbnail, out _)))
                {
                    using (var imageStream = File.OpenRead(photo.AbsolutePath))
                    {
                        _imageProcessor.CreateThumbnails(imageStream, photo.AbsolutePath);
                    }
                }
            }
        }
    }
}
