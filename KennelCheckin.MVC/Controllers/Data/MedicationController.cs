using Kennel.Models.Data.Medication;
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
    public class MedicationController : Controller
    {
        private MedicationService CreateMedicationService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var medicationService = new MedicationService(userId);
            return medicationService;
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
        //    MedicationService service = CreateMedicationService();

        //    var model = await service.GetMedicationById(id);

        //    return View(model);
        //}

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Edit(int id)
        {
            MedicationService service = CreateMedicationService();

            var model = await service.GetMedicationByIdEditable(id);

            return View(model);
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Delete(int id)
        {
            MedicationService service = CreateMedicationService();

            var model = await service.GetMedicationById(id);
            return View(model);
        }

        //Add code here vvvv
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromUri] int id, MedicationCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateMedicationService();

            if (await service.CreateMedication(model))
            {
                await service.CreateMedicationToDogInfo(id);
                return RedirectToAction($"Details/{id}", "DogInfo");
            };

            ModelState.AddModelError("", "Medication could not be added");

            return View(model);
        }

        //Add method here VVVV
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, MedicationEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateMedicationService();

            if (await service.UpdateMedication(id, model))
            {
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Dog could not be edited.");

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteMedication(int id)
        {

            var service = CreateMedicationService();

            DogInfoService infoService = CreateDogInfoService();
            if (await service.DeleteMedication(id))
            {
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Medication could not be deleted.");

            return await Delete(id);
        }
    }
}