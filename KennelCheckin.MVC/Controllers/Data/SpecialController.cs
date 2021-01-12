using Kennel.Models.Data.Special;
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
    [System.Web.Mvc.Authorize(Roles = "Owner,Admin")]
    public class SpecialController : Controller
    {
        private SpecialService CreateSpecialService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var specialService = new SpecialService(userId);
            return specialService;
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
        //    SpecialService service = CreateSpecialService();

        //    var model = await service.GetSpecialById(id);

        //    return View(model);
        //}

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Edit(int id)
        {
            SpecialService service = CreateSpecialService();

            var model = await service.GetSpecialByIdEditable(id);

            return View(model);
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Delete(int id)
        {
            SpecialService service = CreateSpecialService();

            var model = await service.GetSpecialById(id);
            return View(model);
        }

        //Add code here vvvv
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromUri] int id, SpecialCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSpecialService();

            if (await service.CreateSpecial(model))
            {
                DogInfoService infoService = CreateDogInfoService();
                await infoService.UpdateDogInfoAdd(id, "Special");
                return RedirectToAction($"Details/{id}", "DogInfo");
            };

            ModelState.AddModelError("", "Special could not be added");

            return View(model);
        }

        //Add method here VVVV
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SpecialEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateSpecialService();

            if (await service.UpdateSpecial(id, model))
            {
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Dog could not be edited.");

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteSpecial(int id)
        {

            var service = CreateSpecialService();

            DogInfoService infoService = CreateDogInfoService();
            if (await infoService.UpdateDogInfoRemove(id, "Special"))
            {
                await service.DeleteSpecial(id);
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Special could not be deleted.");

            return await Delete(id);
        }
    }
}