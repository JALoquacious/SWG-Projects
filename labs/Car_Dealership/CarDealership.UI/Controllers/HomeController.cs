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
            var featuredList = VehicleRepositoryFactory.GetRepository().GetFeatured();
            return View(featuredList);
        }

        [HttpGet]
        public ActionResult Contact(string VIN = null)
        {
            var contact = new Contact();
            contact.Message = "I'm inquiring about vehicle ID#: " + VIN;

            return View(contact);
        }

        [HttpPost]
        public ActionResult Contact(Contact newContact)
        {
            if (!ModelState.IsValid)
            {
                return View(newContact);
            }

            var repo = ContactRepositoryFactory.GetRepository();

            repo.Insert(newContact);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Specials()
        {
            var specialsList = SpecialRepositoryFactory.GetRepository().GetAll();
            return View(specialsList);
        }
    }
}