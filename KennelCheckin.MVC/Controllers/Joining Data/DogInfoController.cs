﻿using Kennel.Data.Users;
using Kennel.Models.Data.Joining_Data.DogInfo.DisplayOnly;
using Kennel.Service.Joining;
using KennelData.JoiningData;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KennelCheckin.MVC.Controllers.Joining_Data
{
    [Authorize(Roles = "Admin,Owner")]
    public class DogInfoController : Controller
    {

        private DogInfoService CreateDogInfoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var dogInfoService = new DogInfoService(userId);
            return dogInfoService;
        }

        // GET: DogInfo
        public async Task<ActionResult> Index()
        {
            DogInfoService service = CreateDogInfoService();

            if (await service.CheckOwnerExists())//True if there is no owner set up
            {
                return RedirectToAction("Create", "Owner");
            }

            DogInfoIndexView mymodel = await service.DisplayDogInfoIndexView();

            return View(mymodel);
        }

        public async Task<ActionResult> Details(int id)
        {
            DogInfoService service = CreateDogInfoService();

            DogInfoDetails mymodel = await service.GetDogInfoById(id);

            return View(mymodel);
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Delete(int id)
        {
            DogInfoService infoService = CreateDogInfoService();

            var model = await infoService.GetDogInfoById(id);
            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDogInfo(int id)
        {

            if (new ApplicationDbContext().DogVisits.Any(q => q.DogInfoId == id && q.OnSite == true))
            {
                ModelState.AddModelError("", "Dog could not be deleted because it is currently at the kennel");

                return await Delete(id);
            };
            var service = CreateDogInfoService();

            DogInfoService infoService = CreateDogInfoService();
            if (await infoService.DeleteDogInfo(id))
            {
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Dog could not be deleted.");

            return await Delete(id);
        }
    }
}