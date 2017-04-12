using CarDealership.DAL.Factories;
using CarDealership.UI.Models;
using CarDealership.UI.Utilities;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class SalesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var vm = new VehicleSearchViewModel();
            return View(vm);
        }

        [HttpGet]
        public ActionResult Purchase(int id)
        {
            var vehicleRepo = VehicleRepositoryFactory.GetRepository();
            var stateRepo = StateRepositoryFactory.GetRepository();
            var vm = new PurchaseAddViewModel();

            vm.States = new SelectList(stateRepo.GetAll(), "StateId", "Name");
            vm.VehicleDetail = vehicleRepo.GetDetailById(id);

            return View(vm);
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseAddViewModel vm)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserId = AuthorizeUtilities.GetUserId(this);
            }

            if (ModelState.IsValid)
            {
                var repo = VehicleRepositoryFactory.GetRepository();
                repo.Purchase(vm.Sale, vm.Customer);

                return RedirectToAction("Index");
            }
            else
            {
                var vehicleRepo = VehicleRepositoryFactory.GetRepository();
                var stateRepo = StateRepositoryFactory.GetRepository();

                vm.States = new SelectList(stateRepo.GetAll(), "StateId", "Name");
                vm.VehicleDetail = vehicleRepo.GetDetailById(vm.VehicleDetail.VehicleId);

                return View(vm);
            }            
        }
    }
}