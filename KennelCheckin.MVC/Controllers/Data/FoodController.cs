using Kennel.Models.Data.Food;
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
    public class FoodController : Controller
    {
        private FoodService CreateFoodService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var foodService = new FoodService(userId);
            return foodService;
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

        //Add method here VVVV
        //GET
        //public async Task<ActionResult> Details(int id)
        //{
        //    FoodService service = CreateFoodService();

        //    var model = await service.GetFoodById(id);

        //    return View(model);
        //}

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Edit(int id)
        {
            FoodService service = CreateFoodService();

            var model = await service.GetFoodByIdEditable(id);

            return View(model);
        }

        //Add method here VVVV
        //GET
        public async Task<ActionResult> Delete(int id)
        {
            FoodService service = CreateFoodService();

            var model = await service.GetFoodById(id);
            return View(model);
        }

        //Add code here vvvv
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromUri] int id, FoodCreate model)
        {
            if (!ModelState.IsValid || (model.MorningMeal == false && model.EveningMeal == false)) return View(model);

            var service = CreateFoodService();

            if (await service.CreateFood(model))
            {
                DogInfoService infoService = CreateDogInfoService();
                await infoService.UpdateDogInfoAdd(id, "Food");
                return RedirectToAction($"Details/{id}", "DogInfo");
            };

            ModelState.AddModelError("", "Food could not be added");

            return View(model);
        }

        //Add method here VVVV
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, FoodEdit model)
        {
            if (!ModelState.IsValid || (model.MorningMeal == false && model.EveningMeal == false)) return View(model);

            var service = CreateFoodService();

            if (await service.UpdateFood(id, model))
            {
                //TempData["SaveResult"] = "Your note was edited.";
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Food could not be edited.");

            return View(model);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFood(int id)
        {

            var service = CreateFoodService();

            DogInfoService infoService = CreateDogInfoService();
            if (await infoService.UpdateDogInfoRemove(id, "Food"))
            {
                await service.DeleteFood(id);
                return RedirectToAction("Index", "DogInfo");
            };

            ModelState.AddModelError("", "Food could not be deleted.");

            return await Delete(id);
        }
    }
}