using CarDealership.DAL.Factories;
using CarDealership.UI.Utilities;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class InventoryController : Controller
    {
        [HttpGet]
        public ActionResult New()
        {
            //var model = VehicleRepositoryFactory.GetRepository();
            return View();
        }

        [HttpGet]
        public ActionResult Used()
        {
            //var model = VehicleRepositoryFactory.GetRepository();
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserId = AuthorizeUtilities.GetUserId(this);
            }

            var repo = VehicleRepositoryFactory.GetRepository();
            var model = repo.GetById(id);

            return View(model);
        }
    }
}