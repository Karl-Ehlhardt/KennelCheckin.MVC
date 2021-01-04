using Kennel.Models.Data.DogImage;
using Kennel.Models.Joining_Data.DogInfo;
using Kennel.Service.Data;
using Kennel.Service.Joining;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using System.Web.Http;
using Kennel.Data.Users;

namespace KennelCheckin.MVC.Controllers.Data
{
    public class DogImageController : Controller
    {
        private DogImageService CreateDogImageService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var dogImageService = new DogImageService(userId);
            return dogImageService;
        }

        private DogInfoService CreateDogInfoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var dogInfoService = new DogInfoService(userId);
            return dogInfoService;
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Details(int id)
        {
            DogImageService service = CreateDogImageService();

            var model = await service.GetDogImageById(id);

            return View(model);
        }
        public ActionResult RenderPhoto(int photoId)
        {
            byte[] photo = (new ApplicationDbContext()).DogImages.Find(photoId).ImgFile;
            return File(photo, "image/jpeg");
        }

        //Add method here VVVV
        //GET
        //public async Task<ActionResult> Edit(int id)
        //{
        //    DogImageService service = CreateDogImageService();

        //    var model = await service.GetDogImageByIdEditable(id);

        //    return View(model);
        //}

        //Add code here vvvv
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromUri] int id)
        {
            //try
            //{
            //    ImageConverter _imageConverter = new ImageConverter();
            //    byte[] xByte = (byte[])_imageConverter.ConvertTo(x, typeof(byte[]));

            //    MemoryStream target = new MemoryStream();
            //    model.ImgRaw.InputStream.CopyTo(target);
            //    byte[] data = target.ToArray();

            //    var service = CreateDogImageService();

            //    if (await service.CreateDogImage(data))
            //    {
            //        return RedirectToAction("Index", "DogInfo");
            //    };

            //    byte[] imageSize = new byte[file.ContentLength];
            //    file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
            //    Image image = new Image()
            //    {
            //        Name = file.FileName.Split('\\').Last(),
            //        Size = file.ContentLength,
            //        Title = fileTitle,
            //        ID = 1,
            //        Image1 = imageSize
            //    };
            //    db.Images.AddObject(image);
            //    db.SaveChanges();
            //    return RedirectToAction("Detail");
            //}
            //catch (Exception e)
            //{
            //    ModelState.AddModelError("uploadError", e);
            //}
            //return View();

            HttpPostedFileBase file = Request.Files["ImageData"];

            var service = CreateDogImageService();

            if (await service.CreateDogImage(file))
            {
                    DogInfoService infoService = CreateDogInfoService();
                    await infoService.UpdateDogInfoAdd(id, "DogImage");
                    return RedirectToAction($"Details/{id}", "DogInfo");
            };

            ModelState.AddModelError("", "Image could not be added");

            return View();
        }

        //Add method here VVVV
        //[HttpPost]
        //[ActionName("Edit")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit(int id, DogImageEdit model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    var service = CreateDogImageService();

        //    if (await service.UpdateDogImage(id, model))
        //    {
        //        //TempData["SaveResult"] = "Your note was edited.";
        //        return RedirectToAction("Index", "DogInfo");
        //    };

        //    ModelState.AddModelError("", "Dog could not be edited.");

        //    return View(model);
        //}
    }
}