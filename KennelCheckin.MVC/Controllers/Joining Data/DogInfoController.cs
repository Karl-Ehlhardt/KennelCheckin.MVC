using Kennel.Models.Data.DisplayOnly;
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
    }
}