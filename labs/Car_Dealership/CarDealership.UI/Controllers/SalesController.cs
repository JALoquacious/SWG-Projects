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
            vm.SearchParams.IsAspNetUser = true;
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
            //vm.Customer.UserId = "00000000-0000-0000-0000-000000000000"; // load ASP.Net User here

            return View(vm);
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseAddViewModel vm)
        {
            var adminManager = AdminManagerFactory.GetRepository();
            var vehicleRepo  = VehicleRepositoryFactory.GetRepository();
            vm.VehicleDetail = vehicleRepo.GetDetailById(vm.VehicleDetail.VehicleId);

            if (Request.IsAuthenticated)
            {
                ViewBag.UserId = AuthorizeUtilities.GetUserId(this);
            }

            if (ModelState.IsValid)
            {
                adminManager.Purchase(vm.VehicleDetail, vm.Sale, vm.Customer);
                //vm.Sale.UserId = ViewBag.UserId; get User Id this way?
                return RedirectToAction("Index");
            }
            else
            {
                var stateRepo = StateRepositoryFactory.GetRepository();
                vehicleRepo = VehicleRepositoryFactory.GetRepository();

                vm.States = new SelectList(stateRepo.GetAll(), "StateId", "Name");
                vm.VehicleDetail = vehicleRepo.GetDetailById(vm.VehicleDetail.VehicleId);

                return View(vm);
            }            
        }
    }
}