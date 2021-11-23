using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageUploadDelete.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Data;

namespace ImageUploadDelete.Controllers
{
    public class ImageController : Controller
    {

        private readonly IWebHostEnvironment _iweb;

        public ImageController(IWebHostEnvironment iweb)
        {
            _iweb = iweb;

        }
        public IActionResult Index()
        {
            ImageClass ic = new ImageClass();
            var dislayImage = Path.Combine(_iweb.WebRootPath, "Images");

            DirectoryInfo di = new DirectoryInfo(dislayImage);
            FileInfo[] fileInfo = di.GetFiles();

            ic.fileImage = fileInfo;

            return View(ic);
        }

        [HttpPost]

        public async Task<ActionResult> Index(IFormFile imgFile)
        {
            string ext = Path.GetExtension(imgFile.FileName);
            if (ext == ".bmp" || ext == ".ico" || ext == ".jpeg" || ext == ".jpg" || ext == ".gif" || ext == ".tiff" || ext == ".png")
            {
                var imageSave = Path.Combine(_iweb.WebRootPath, "Images", imgFile.FileName);
                var stream = new FileStream(imageSave, FileMode.Create);

                await imgFile.CopyToAsync(stream);

                stream.Close();
            }

            else
            {
                ViewBag.ErrorMessage = "YOU HAVE SELECTED AN INVALID FILE. PLEASE SELECT AN IMAGE OR GIF";
                return View();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string imageDelete)
        {
            imageDelete = Path.Combine(_iweb.WebRootPath, "Images", imageDelete);
            FileInfo fi = new FileInfo(imageDelete);
            if (fi != null)
            {
                System.IO.File.Delete(imageDelete);
                fi.Delete();
            }
            return RedirectToAction("Index");
        }

    }
}
