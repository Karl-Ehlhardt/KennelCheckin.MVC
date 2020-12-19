using Kennel.Models.Data.Owner;
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
    public class OwnerController : Controller
    {
        private OwnerService CreateOwnerService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var ownerService = new OwnerService(userId);
            return ownerService;
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Create()
        {
            OwnerService service = CreateOwnerService();
            if (await service.CheckOwnerExists())//True if there is no owner set up
            {
                return View();
            }
            return RedirectToAction("Index", "DogInfo");
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Details(int id)
        {
            OwnerService service = CreateOwnerService();

            var model = await service.GetOwnerById(id);

            return View(model);
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Edit(int id)
        {
            OwnerService service = CreateOwnerService();

            var model = await service.GetOwnerByIdEditable(id);

            return View(model);
        }

        //Add code here vvvv
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OwnerCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateOwnerService();

            if (await service.CreateOwner(model))
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
        public async Task<ActionResult> Edit(int id, OwnerEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateOwnerService();

            if (await service.UpdateOwner(id, model))
            {
                TempData["SaveResult"] = "Your note was edited.";
                return RedirectToAction("Create");
            };

            ModelState.AddModelError("", "Note could not be edited.");

            return View(model);
        }
    }
}