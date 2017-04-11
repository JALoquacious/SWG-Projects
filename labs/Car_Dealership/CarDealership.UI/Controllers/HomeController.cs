using CarDealership.DAL.Factories;
using CarDealership.Models.Tables;
using CarDealership.UI.Models;
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
            var vm = new ContactAddViewModel();
            vm.Message = "I'm inquiring about vehicle ID#: " + VIN;
            vm.Contact = new Contact();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Contact(ContactAddViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var repo = ContactRepositoryFactory.GetRepository();
            var newContact = vm.Contact;

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