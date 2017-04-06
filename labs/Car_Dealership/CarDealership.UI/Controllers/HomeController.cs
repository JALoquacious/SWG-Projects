using CarDealership.DAL.Factories;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = VehicleRepositoryFactory.GetRepository().GetFeatured();
            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}