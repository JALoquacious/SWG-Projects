using CarDealership.DAL.Factories;
using CarDealership.Models.Enums;
using CarDealership.UI.Models;
using CarDealership.UI.Utilities;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class InventoryController : Controller
    {
        [HttpGet]
        public ActionResult New()
        {
            var vm = new VehicleSearchViewModel();
            vm.SearchParams.Condition = (int)Condition.New;
            return View(vm);
        }

        [HttpGet]
        public ActionResult Used()
        {
            var vm = new VehicleSearchViewModel();
            vm.SearchParams.Condition = (int)Condition.Used;
            return View(vm);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserId = AuthorizeUtilities.GetUserId(this);
            }

            var repo = VehicleRepositoryFactory.GetRepository();
            var model = repo.GetDetailById(id);

            return View(model);
        }
    }
}