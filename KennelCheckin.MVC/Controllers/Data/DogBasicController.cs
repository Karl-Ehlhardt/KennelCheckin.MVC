using Kennel.Models.Data.DogBasic;
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

namespace KennelCheckin.MVC.Controllers.Data
{
    public class DogBasicController : Controller
    {
        private DogBasicService CreateDogBasicService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var dogBasicService = new DogBasicService(userId);
            return dogBasicService;
        }

        private DogInfoService CreateDogInfoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var dogInfoService = new DogInfoService(userId);
            return dogInfoService;
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Create()
        {
            DogBasicService service = CreateDogBasicService();
            if (await service.CheckOwner())//True if there is no owner set up
            {
                return RedirectToAction("Create", "Owner");
            }
            return View();
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Details(int id)
        {
            DogBasicService service = CreateDogBasicService();

            var model = await service.GetDogBasicById(id);

            return View(model);
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Edit(int id)
        {
            DogBasicService service = CreateDogBasicService();

            var model = await service.GetDogBasicByIdEditable(id);

            return View(model);
        }

        //Add code here vvvv
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DogBasicCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateDogBasicService();

            if (await service.CreateDogBasic(model))
            {
                DogInfoService infoService = CreateDogInfoService();
                int newDogInfoId = await infoService.CreateDogInfo();
                return RedirectToAction($"Details/{newDogInfoId}", "DogInfo");
            };

            ModelState.AddModelError("", "Dog could not be added");

            return View(model);
        }

        //Add method here VVVV
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DogBasicEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateDogBasicService();

            if (await service.UpdateDogBasic(id, model))
            {
                //TempData["SaveResult"] = "Your note was edited.";
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Dog could not be edited.");

            return View(model);
        }
    }
}