using Kennel.Models.Joining_Data.DogVisit;
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

namespace KennelCheckin.MVC.Controllers.Joinging_Data
{
    [System.Web.Mvc.Authorize(Roles = "Owner,Admin")]
    public class DogVisitController : Controller
    {
        private DogVisitService CreateDogVisitService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var dogVisitService = new DogVisitService(userId);
            return dogVisitService;
        }
        private DogInfoService CreateDogInfoService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var dogInfoService = new DogInfoService(userId);
            return dogInfoService;
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Create()//ID of the DogInfo
        {
            return View();
        }

        ////Add code here vvvv
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromUri] int id, DogVisitCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (DateTime.Compare(model.DropOffTime, model.PickUpTime) >= 0)
            {
                ModelState.AddModelError("", "Dropoff date must be before pickup");

                return View(model);
            }

            var service = CreateDogVisitService();

            if (await service.CreateDogVisit(id, model))
            {
                return RedirectToAction($"Index", "DogInfo");
            };

            ModelState.AddModelError("", "DogVisit could not be added");

            return View(model);
        }

        ////Add method here VVVV
        ////GET
        //public async Task<ActionResult> Details(int id)
        //{
        //    DogVisitService service = CreateDogVisitService();

        //    var model = await service.GetDogVisitById(id);

        //    return View(model);
        //}

        ////Add method here VVVV
        ////GET
        public async Task<ActionResult> Edit(int id)
        {
            DogVisitService service = CreateDogVisitService();

            var model = await service.GetDogVisitByIdEditable(id);

            return View(model);
        }

        //Add method here VVVV
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DogVisitEdit model)
        {
            if (!ModelState.IsValid)
            {
                DogVisitService remedialService = CreateDogVisitService();

                var remedialmodel = await remedialService.GetDogVisitByIdEditable(id);

                return View(remedialmodel);
            }


            if (DateTime.Compare(model.DropOffTime, model.PickUpTime) >= 0)
            {
                ModelState.AddModelError("", "Dropoff date must be before pickup");

                return View(model);
            }

            var service = CreateDogVisitService();

            if (await service.UpdateDogVisit(id, model))
            {
                //TempData["SaveResult"] = "Your note was edited.";
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Dog Visit could not be edited.");

            return View(model);
        }

        ////GET
        public async Task<ActionResult> ChangePickup(int id)
        {
            DogVisitService service = CreateDogVisitService();

            var model = await service.GetDogVisitByIdChangePickup(id);

            return View(model);
        }

        //Add method here VVVV
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("ChangePickup")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePickup(int id, DogVisitChangePickup model)
        {
                
            DogVisitService service = CreateDogVisitService();

            if (!ModelState.IsValid)
            {
                var original = await service.GetDogVisitByIdChangePickup(id);
                return View(original);
            }

            if (DateTime.Now.Date> model.PickUpTime)
            {
                ModelState.AddModelError("", "New pick-up date must be today or later");

                var original = await service.GetDogVisitByIdChangePickup(id);

                return View(original);
            }
            else if(await service.ChangePickupDogVisit(id, model))
            {
                return RedirectToAction("Index", "DogInfo");
            }
            else
            {
                ModelState.AddModelError("", "Dog Visit could not be edited.");
                var original = await service.GetDogVisitByIdChangePickup(id);
                return View(original);
            }

        }

        ////Add method here VVVV
        ////GET
        public async Task<ActionResult> Delete(int id)
        {
            DogVisitService service = CreateDogVisitService();

            var model = await service.GetDogVisitById(id);
            return View(model);
        }



        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDogVisit(int id)
        {

            var service = CreateDogVisitService();

            if (await service.DeleteDogVisit(id))
            {
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "DogVisit could not be deleted.");

            return await Delete(id);
        }

        //=============Helper=======//
        //public async Task<ActionResult> DogNameLookUp(int dogInfoId)
        //{
        //    DogVisitService service = CreateDogVisitService();
        //    var dogName = await service.GetDogName(dogInfoId);
        //    return Content($"{dogName}");
        //}
    }
}
