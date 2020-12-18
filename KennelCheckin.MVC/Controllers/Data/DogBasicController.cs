using Kennel.Models.Data.DogBasic;
using Kennel.Service.Data;
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

        //Add method here VVVV
        //GET
        public ActionResult Create()
        {
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

            var model = await service.GetDogBasicById(id);
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
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Create");
            };

            ModelState.AddModelError("", "Note could not be created.");

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
                TempData["SaveResult"] = "Your note was edited.";
                return RedirectToAction("Create");
            };

            ModelState.AddModelError("", "Note could not be edited.");

            return View(model);
        }
    }
}