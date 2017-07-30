using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotoGallery.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoGallery.Pages
{
    public class AlbumsModel : PageModel
    {
        private AlbumCollection _ac;
        private IHostingEnvironment _environment;
        private ImageProcessor _processor;

        public AlbumsModel(AlbumCollection ac, IHostingEnvironment environment, ImageProcessor processor)
        {
            _ac = ac;
            _environment = environment;
            _processor = processor;
        }

        public Album Album { get; private set; }

        public void OnGet(string name)
        {
            Album = _ac.Albums.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            ViewData["Title"] = Album.Name;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult OnPostDelete(string name)
        {
            string path = Path.Combine(_environment.WebRootPath, "albums", name);

            if (Directory.Exists(path))
                Directory.Delete(path, true);

            var album = _ac.Albums.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (album != null)
            {
                _ac.Albums.Remove(album);
            }

            return new RedirectResult("~/");
        }

        [Authorize]
        public IActionResult OnPostCreate(string name)
        {
            string path = Path.Combine(_environment.WebRootPath, "albums", name);

            Directory.CreateDirectory(path);
            var album = new Album(path, _ac);
            _ac.Albums.Insert(0, album);

            return new RedirectResult($"~/album/{name}/");
        }

        [Authorize]
        public async Task<IActionResult> OnPostUpload(string name, ICollection<IFormFile> files)
        {
            foreach (var file in files.Where(f => _ac.IsImageFile(f.FileName)))
            {
                var album = _ac.Albums.FirstOrDefault(a => a.UrlName.Equals(name, StringComparison.OrdinalIgnoreCase));
                string path = Path.Combine(_environment.WebRootPath, "albums", album.Name, Path.GetFileName(file.FileName));

                if (System.IO.File.Exists(path))
                {
                    path = Path.ChangeExtension(path, file.GetHashCode() + Path.GetExtension(path));
                }

                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(stream);
                }

                _processor.CreateThumbnails(name, path);

                var photo = new Photo(album, new FileInfo(path));
                album.Photos.Insert(0, photo);
            }

            return new RedirectResult($"~/album/{name}/");
        }
    }
}
