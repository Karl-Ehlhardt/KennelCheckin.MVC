using Kennel.Models.Data.Kennel.KennelDashboardItems;
using Kennel.Service.Joining;
using Kennel.Service.Kennel;
using KennelData.Data;
using KennelData.JoiningData;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace KennelCheckin.MVC.Controllers.KennelControllers
{
    public class KennelController : Controller
    {

        private KennelService CreateKennelService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var kennelService = new KennelService(userId);
            return kennelService;
        }

        //// GET: Kennel
        public async Task<ActionResult> Dashboard()
        {
            KennelService service = CreateKennelService();

            KennelDashboardView mymodel = await service.DisplayKennelDashboardView();

            return View(mymodel);
        }

        //GET
        public async Task<ActionResult> DogDetails([FromUri] int id)
        {
            KennelService service = CreateKennelService();

            KennelDogDetails mymodel = await service.KennelDogDetailsByDogVisitId(id);

            return View(mymodel);
        }

        //GET
        public async Task<ActionResult> CheckIn([FromUri] int id)
        {
            KennelService service = CreateKennelService();

            DogBasic mymodel = await service.GetDogBasicByDogVisitId(id);

            return View(mymodel);
        }

        //POST
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("CheckIn")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckIn([FromUri] int id, string blank)
        {
            KennelService service = CreateKennelService();

            if (await service.CheckInDogVisitById(id))
            {
                return RedirectToAction($"Dashboard");
            };

            ModelState.AddModelError("", "Dog could not be checked-in");

            DogBasic model = await service.GetDogBasicByDogVisitId(id);

            return View(model);
        }

        //GET
        public async Task<ActionResult> CheckOut([FromUri] int id)
        {
            KennelService service = CreateKennelService();

            DogBasic mymodel = await service.GetDogBasicByDogVisitId(id);

            return View(mymodel);
        }

        //POST
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("CheckOut")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckOut([FromUri] int id, string blank)
        {
            KennelService service = CreateKennelService();

            if (await service.CheckOutDogVisitById(id))
            {
                return RedirectToAction($"Dashboard");
            };

            ModelState.AddModelError("", "Dog could not be checked-out");

            DogBasic model = await service.GetDogBasicByDogVisitId(id);

            return View(model);
        }

        //action that does not load a page, returns to dashboard
        public async Task<ActionResult> ResetVisit([FromUri] int id)
        {
            KennelService service = CreateKennelService();

            if (await service.ResetDogVisitById(id))
            {
                return RedirectToAction("Dashboard");
            };

            return View();
        }

        //GET
        public async Task<ActionResult> MorningMealsAndMeds()
        {
            KennelService service = CreateKennelService();

            var mymodel = await service.AllMorningMealsAndMeds();

            return View(mymodel);
        }

        //GET
        public async Task<ActionResult> EveningMealsAndMeds()
        {
            KennelService service = CreateKennelService();

            var mymodel = await service.AllEveningMealsAndMeds();

            return View(mymodel);
        }

        //public async Task<ActionResult> Details(int id)
        //{
        //    KennelService service = CreateKennelService();

        //    KennelDetails mymodel = await service.GetKennelById(id);

        //    return View(mymodel);
        //}

        ////Add method here VVVV
        ////GET
        //public async Task<ActionResult> Delete(int id)
        //{
        //    KennelService infoService = CreateKennelService();

        //    var model = await infoService.GetKennelById(id);
        //    return View(model);
        //}

        //[System.Web.Mvc.HttpPost]
        //[System.Web.Mvc.ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteKennel(int id)
        //{

        //    var service = CreateKennelService();

        //    KennelService infoService = CreateKennelService();
        //    if (await infoService.DeleteKennel(id))
        //    {
        //        return RedirectToAction("Index", "Kennel");
        //    };

        //    ModelState.AddModelError("", "Food could not be deleted.");

        //    return await Delete(id);
        //}
    }
}