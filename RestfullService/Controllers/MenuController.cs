using Sistem.Service.MenuK;
using System.Net;
using System.Web.Http;
namespace RestfullService.Controllers
{
    public class MenuController : ApiController
    {
        private readonly IMenu menuService;
        public MenuController(IMenu menuService)
        {
            this.menuService = menuService;
        }
        public IHttpActionResult Get()
        {
            return Content(HttpStatusCode.OK, menuService.GetAllItem());
        }
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            return Content(HttpStatusCode.OK, menuService.GetMenuAndItemsByMenuId(id));
        }
    }
}