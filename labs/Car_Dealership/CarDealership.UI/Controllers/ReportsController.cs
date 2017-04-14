using CarDealership.DAL.Factories;
using CarDealership.UI.Models;
using System.Linq;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class ReportsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Sales()
        {
            var vm      = new SalesReportViewModel();
            var context = new ApplicationDbContext();
            var manager = AdminManagerFactory.GetManager();
            var users   = context.Users;

            vm.Users = new SelectList(users, "Id", "UserName");
            vm.Sales = manager.GetSalesReport();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Sales(SalesReportViewModel vm)
        {
            var manager = AdminManagerFactory.GetManager();
            var context = new ApplicationDbContext();
            var users = context.Users;

            if (ModelState.IsValid)
            {
                vm.Sales = manager.FilterSalesReport(vm.UserId, vm.FromDate, vm.ToDate);
                vm.Users = new SelectList(users, "Id", "UserName");

                return View(vm);
            }
            else
            {
                manager  = AdminManagerFactory.GetManager();
                vm       = new SalesReportViewModel();
                vm.Users = new SelectList(users, "Id", "UserName");
                vm.Sales = manager.GetSalesReport();

                return View(vm);
            }
        }

        [HttpGet]
        public ActionResult Inventory()
        {
            var manager = AdminManagerFactory.GetManager();
            var vm      = new InventoryReportViewModel();

            vm.NewInventory  = manager.GetInventoryReport(false);
            vm.UsedInventory = manager.GetInventoryReport(true);

            return View(vm);
        }
    }
}