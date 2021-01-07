using Kennel.Service.Joining;
using Kennel.Service.Kennel;
using KennelData.JoiningData;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace KennelCheckin.MVC.Controllers.KennelControllers
{
    public class KennelController : Controller
    {

        //private KennelService CreateKennelService()
        //{
        //    var userId = Guid.Parse(User.Identity.GetUserId());
        //    var kennelService = new KennelService(userId);
        //    return kennelService;
        //}

        //// GET: Kennel
        //public async Task<ActionResult> Dashboard()
        //{
        //    KennelService service = CreateKennelService();

        //    if (await service.CheckOwnerExists())//True if there is no owner set up
        //    {
        //        return RedirectToAction("Create", "Owner");
        //    }

        //    KennelDashboardView mymodel = await service.DisplayKennelDashboardView();

        //    return View(mymodel);
        //}

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