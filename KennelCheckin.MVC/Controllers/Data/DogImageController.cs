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
using System.Web.Security;

namespace KennelCheckin.MVC.Controllers.Data
{
    [System.Web.Mvc.Authorize(Roles = "Owner,Admin")]
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
        public ActionResult RenderPhoto(int photoId)
        {
            byte[] photo = (new ApplicationDbContext()).DogImages.Find(photoId).ImgFile;
            return File(photo, "image/jpeg");
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        //Add code here vvvv
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromUri] int id)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];

            if (file.ContentLength == 0) return View();

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
        //GET
        //public async Task<ActionResult> Details(int id)
        //{
        //    DogImageService service = CreateDogImageService();

        //    var model = await service.GetDogImageById(id);

        //    return View(model);
        //}


        //Add method here VVVV
        //GET
        public async Task<ActionResult> Edit(int id)
        {
            var service = CreateDogImageService();

            var model = await service.GetDogImageIdEdit(id);

            return View(model);
        }


        //Add method here VVVV
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, string nothing)
        {
            HttpPostedFileBase file = Request.Files["ImageData"];

            var service = CreateDogImageService();

            if (file.ContentLength == 0) return RedirectToAction("Edit");

            if (await service.UpdateDogImage(id, file))
            {
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Image could not be replaced.");

            return RedirectToAction("Edit");
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Delete(int id)
        {
            var service = CreateDogImageService();

            var model = await service.GetDogImageIdEdit(id);
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDogImage(int id)
        {

            var service = CreateDogImageService();

            DogInfoService infoService = CreateDogInfoService();
            if (await infoService.UpdateDogInfoRemove(id, "DogImage"))
            {
                await service.DeleteDogImage(id);
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Image could not be deleted.");

            return await Delete(id);
        }
    }
}