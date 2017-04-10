using CarDealership.DAL.Factories;
using CarDealership.Models.Tables;
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
            return View();
        }

        [HttpGet]
        public ActionResult Purchase(int id)
        {
            var repo = VehicleRepositoryFactory.GetRepository();
            var vm   = new PurchaseAddViewModel();

            vm.Customer      = new Customer();
            vm.PaymentType   = new PaymentType();
            vm.Sale          = new Sale();
            vm.State         = new State();
            vm.Vehicle       = repo.GetById(id);
            vm.VehicleDetail = repo.GetDetailById(id);

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
                // assign SaleId to vehicle
                // create stored procedure for making sale?
                return RedirectToAction("Index");
            }
            else
            {
                return View(vm);
            }            
        }
    }
}