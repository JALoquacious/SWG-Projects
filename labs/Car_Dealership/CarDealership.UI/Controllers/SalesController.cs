using CarDealership.DAL.Factories;
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
            if (Request.IsAuthenticated)
            {
                ViewBag.UserId = AuthorizeUtilities.GetUserId(this);
            }

            // need to pass VehiclePurchaseViewModel rather than VehicleDetail model?
            var repo = VehicleRepositoryFactory.GetRepository();
            var model = repo.GetById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Purchase()
        {
            if (Request.IsAuthenticated)
            {
                ViewBag.UserId = AuthorizeUtilities.GetUserId(this);
            }

            // need to pass VehiclePurchaseViewModel?

            return RedirectToAction("Index");
        }
    }
}