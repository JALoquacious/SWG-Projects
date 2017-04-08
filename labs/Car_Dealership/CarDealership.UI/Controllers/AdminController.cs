using CarDealership.DAL.Factories;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Specials()
        {
            var repo = SpecialRepositoryFactory.GetRepository();
            var vm = new SpecialAddViewModel();

            vm.SpecialsList = repo.GetAll();
            vm.NewSpecial = new Special();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Specials(Special newSpecial)
        {
            var repo = SpecialRepositoryFactory.GetRepository();
            repo.Insert(newSpecial);
            return RedirectToAction("Specials");
        }

        [HttpGet]
        public ActionResult Vehicles()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddVehicle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddVehicle(int vehicleId)
        {
            //var repo = VehicleRepositoryFactory.GetRepository();
            return View();
        }
    }
}