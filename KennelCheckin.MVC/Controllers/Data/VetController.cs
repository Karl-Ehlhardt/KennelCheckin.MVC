using Kennel.Models.Data.Vet;
using Kennel.Models.Joining_Data.DogInfo;
using Kennel.Service.Data;
using Kennel.Service.Joining;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KennelCheckin.MVC.Controllers.Data
{
    public class VetController : Controller
    {
        private VetService CreateVetService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var vetService = new VetService(userId);
            return vetService;
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
            return View();
        }

        //Add method here VVVV
        //GET
        //public async Task<ActionResult> Details(int id)
        //{
        //    VetService service = CreateVetService();

        //    var model = await service.GetVetById(id);

        //    return View(model);
        //}

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Edit(int id)
        {
            VetService service = CreateVetService();

            var model = await service.GetVetByIdEditable(id);

            return View(model);
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Delete(int id)
        {
            VetService service = CreateVetService();

            var model = await service.GetVetById(id);
            return View(model);
        }

        //Add code here vvvv
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromUri] int id, VetCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateVetService();

            if (await service.CreateVet(model))
            {
                DogInfoService infoService = CreateDogInfoService();
                await infoService.UpdateDogInfoAdd(id, "Vet");
                return RedirectToAction($"Details/{id}", "DogInfo");
            };

            ModelState.AddModelError("", "Vet could not be added");

            return View(model);
        }

        //Add method here VVVV
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, VetEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateVetService();

            if (await service.UpdateVet(id, model))
            {
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Dog could not be edited.");

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteVet(int id)
        {

            var service = CreateVetService();

            DogInfoService infoService = CreateDogInfoService();
            if (await infoService.UpdateDogInfoRemove(id, "Vet"))
            {
                await service.DeleteVet(id);
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Vet could not be deleted.");

            return await Delete(id);
        }
    }
}