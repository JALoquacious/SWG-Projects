using CarDealership.DAL.Factories;
using CarDealership.Models.Tables;
using System.Web.Mvc;

namespace CarDealership.UI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = VehicleRepositoryFactory.GetRepository().GetFeatured();
            return View(model);
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Contact newContact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var repo = ContactRepositoryFactory.GetRepository();

            repo.Insert(newContact);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Specials()
        {
            var model = SpecialRepositoryFactory.GetRepository().GetAll();
            return View(model);
        }
    }
}