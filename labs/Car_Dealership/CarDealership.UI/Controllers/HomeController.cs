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
            vm.Contact = new Contact();
            vm.Contact.Message = "I'm inquiring about vehicle ID#: " + VIN;

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

            repo.Insert(vm.Contact);

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