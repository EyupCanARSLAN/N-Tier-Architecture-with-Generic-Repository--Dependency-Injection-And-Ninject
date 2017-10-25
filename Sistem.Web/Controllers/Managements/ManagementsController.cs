using System.Web.Mvc;
namespace Sistem.Web.Controllers.Managements
{
    [Authorize(Roles = "Managements")]
    public class ManagementsController : Controller
    {
        // GET: Managements
        public ActionResult Index()
        {
            return View();
        }
    }
}