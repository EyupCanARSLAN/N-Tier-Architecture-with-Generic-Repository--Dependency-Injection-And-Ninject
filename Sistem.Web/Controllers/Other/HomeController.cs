using System.Web.Mvc;
namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (User.IsInRole("Customer")) return RedirectToAction("Index", "CustomerMenu");
            if (User.IsInRole("Managements")) return RedirectToAction("Index", "Managements");
            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
