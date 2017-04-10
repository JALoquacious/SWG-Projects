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
            return View();
        }

        [HttpGet]
        public ActionResult Inventory()
        {
            return View();
        }
    }
}