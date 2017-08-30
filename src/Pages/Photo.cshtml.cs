using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoGallery.Models;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace PhotoGallery.Pages
{
    public class PhotoModel : PageModel
    {
        private AlbumCollection _ac;
        private IHostingEnvironment _environment;

        public PhotoModel(AlbumCollection ac, IHostingEnvironment environment)
        {
            _ac = ac;
            _environment = environment;
        }

        public Photo Photo { get; set; }

        public void OnGet(string albumName, string photoName)
        {
            var album = _ac.Albums.FirstOrDefault(a => a.Name.Equals(albumName, StringComparison.OrdinalIgnoreCase));
            Photo = album.Photos.FirstOrDefault(p => p.DisplayName.Equals(photoName, StringComparison.OrdinalIgnoreCase));
        }

        public IActionResult OnPostRename(string albumName, string photoName)
        {
            var album = _ac.Albums.FirstOrDefault(a => a.Name.Equals(albumName, StringComparison.OrdinalIgnoreCase));
            Photo = album.Photos.FirstOrDefault(p => p.DisplayName.Equals(photoName, StringComparison.OrdinalIgnoreCase));
            string name = Request.Form["name"] + Path.GetExtension(Photo.AbsolutePath);

            var newPhotoPath = new FileInfo(Path.Combine(album.AbsolutePath, name));
            int index = album.Photos.IndexOf(Photo);

            System.IO.File.Move(Photo.AbsolutePath, newPhotoPath.FullName);
            var newPhoto = new Photo(album, newPhotoPath);

            album.Photos.Insert(index, newPhoto);
            album.Photos.RemoveAt(index + 1);

            // Rename thumbnails
            string folder = Path.Combine(album.AbsolutePath, "thumbnail");
            var pattern = $"{Photo.DisplayName}-*x*{Path.GetExtension(Photo.AbsolutePath)}";

            foreach (var file in Directory.EnumerateFiles(folder, pattern))
            {
                string newThumbnail = Path.Combine(folder, Path.GetFileName(file).Replace(Photo.DisplayName, newPhoto.DisplayName));
                System.IO.File.Move(file, newThumbnail);
            }

            return new RedirectResult($"~/photo/{WebUtility.UrlEncode(albumName).Replace('+', ' ')}/{newPhoto.DisplayName}/");

        }

        public IActionResult OnPostDelete(string albumName, string photoName)
        {
            var album = _ac.Albums.FirstOrDefault(a => a.Name.Equals(albumName, StringComparison.OrdinalIgnoreCase));
            Photo = album.Photos.FirstOrDefault(p => p.DisplayName.Equals(photoName, StringComparison.OrdinalIgnoreCase));
            album.Photos.Remove(Photo);

            if (System.IO.File.Exists(Photo.AbsolutePath))
            {
                System.IO.File.Delete(Photo.AbsolutePath);
                string folder = Path.Combine(album.AbsolutePath, "thumbnail");
                var pattern = $"{Photo.DisplayName}-*x*{Path.GetExtension(Photo.AbsolutePath)}";

                foreach (var file in Directory.EnumerateFiles(folder, pattern))
                {
                    System.IO.File.Delete(file);
                }
            }

            return new RedirectResult($"~/album/{WebUtility.UrlEncode(albumName).Replace('+', ' ')}/");
        }
    }
}
