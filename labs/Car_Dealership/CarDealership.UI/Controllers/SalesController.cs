using CarDealership.DAL.Factories;
using CarDealership.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    [Authorize(Roles = "sales")]
    public class SalesController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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
            vm.Customer.UserId = User.Identity.GetUserId();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Purchase(PurchaseAddViewModel vm)
        {
            var adminManager = AdminManagerFactory.GetManager();
            var vehicleRepo  = VehicleRepositoryFactory.GetRepository();
            vm.VehicleDetail = vehicleRepo.GetDetailById(vm.VehicleDetail.VehicleId);

            if (ModelState.IsValid)
            {
                vm.Sale.UserId = User.Identity.GetUserId();
                adminManager.Purchase(vm.VehicleDetail, vm.Sale, vm.Customer);

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