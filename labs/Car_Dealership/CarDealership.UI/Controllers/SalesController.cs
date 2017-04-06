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
    }
}