using Sistem.Core.Data.MenuK;
using Sistem.Service.MenuK;
using Sistem.Web.Models.ViewModel.Managements.MenuSection;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
namespace Sistem.Web.Controllers.Managements.MenuSection
{
    /// <summary>
    /// At this Controller; Manager can define and update Menu and Products, can view Statistic About Menu
    /// </summary>
    [Authorize(Roles = "Managements")]
    public class MenuProcessController : Controller
    {
        private readonly IMenu MenuService;
        public MenuProcessController(IMenu IMenuService)
        {
            MenuService = IMenuService;
        }
        #region MenuIslemleri
        public ActionResult Index(int? ErrorProcess)
        {
            if (ErrorProcess == 1) ViewBag.ErrorOccur = "Unexpedted error occur. May you try to enter inccorect type value for the input field";
            return View("~/Views/Managements/MenuSection/MenuProcess/Index.cshtml", MenuService.GetAllItem());
        }
        public ActionResult CreateMenu()
        {
            return View("~/Views/Managements/MenuSection/MenuProcess/CreateMenu.cshtml");
        }
        [HttpPost]
        public ActionResult CreateMenu(MenuProcessModel model)
        {
            if (!ModelState.IsValid) return View(model);
            Menu coreDataMenu = new Menu
            {
                MenuName = model.MenuName,
                Price = model.Price
            };
            MenuService.InsertMenu(coreDataMenu);
            return View("~/Views/Managements/MenuSection/MenuProcess/CreateMenu.cshtml");
        }
        [HttpGet]
        public ActionResult UrunListForMenu(int? MenuId)
        {
            if (MenuId == null) throw new Exception("Missing Url Parameter.");
            var selectedMenu = MenuService.GetMenuAndItemsByMenuId(MenuId.Value).FirstOrDefault();
            if (selectedMenu == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("~/Views/Managements/MenuSection/MenuProcess/UrunListForMenu.cshtml", selectedMenu);
        }
        [HttpGet]
        public ActionResult IstatistikForMenu(int? MenuId)
        {
            if (MenuId == null) throw new Exception("Missing Url Parameter.");
            var selectedMenu = MenuService.GetMenuStatisticByMenuId(MenuId.Value).FirstOrDefault();
            if (selectedMenu == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            return View("~/Views/Managements/MenuSection/MenuProcess/IstatistikForMenu.cshtml", selectedMenu);
        }
        [HttpGet]
        public ActionResult InsertUrunInMenu(int? MenuId)
        {
            if (MenuId == null) throw new Exception("Missing Url Parameter.");
            var UrunListinThisMenu = MenuService.GetMenuAndItemsByMenuId(MenuId.Value).First();
            #region DeleteSameUrun - Daha önceden eklenmiş ürünleri sildi...
            var newUrunList = MenuService.GetAllItems().ToList();
            foreach (var item in UrunListinThisMenu.Product)
            {
                newUrunList.RemoveAll(x => x.ProductId == item.ProductId);
            }
            #endregion
            ViewBag.SelectedMenu = MenuId;
            return View("~/Views/Managements/MenuSection/MenuProcess/InsertUrunInMenu.cshtml", newUrunList);
        }
        [HttpPost, ActionName("UrunListForMenu")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateMenuGeneralInformation([Bind(Include = "MenuId,MenuAd,Fiyat")]Menu UpdatedMenu)
        {
            if (String.IsNullOrEmpty(UpdatedMenu.MenuName)) return RedirectToAction("Index", new { ErrorProcess = 1 });
            if (!ModelState.IsValid) return RedirectToAction("Index", new { ErrorProcess = 1 });
            var selecetedMenuForUpdate = MenuService.GetMenuById(UpdatedMenu.MenuId);
            selecetedMenuForUpdate.MenuName = UpdatedMenu.MenuName;
            selecetedMenuForUpdate.Price = UpdatedMenu.Price;
            MenuService.UpdateMenu(selecetedMenuForUpdate);
            return RedirectToAction("UrunListForMenu", new { MenuId = UpdatedMenu.MenuId });
        }
        [HttpGet]
        public ActionResult SaveUrunInMenu(int? MenuId, int? UrunId)
        {
            if (MenuId == null || UrunId == null) throw new Exception("Missing Url Parameter.");
            var addedSuccesful = MenuService.InsertProductInMenu(UrunId.Value, MenuId.Value);
            var text = addedSuccesful ? "Your adding process is successfull." : "This Product was added in menu before!";
            ViewBag.AddingProcess = text;
            return RedirectToAction("InsertUrunInMenu", new { MenuId = MenuId });
        }
        public ActionResult DeleteUrunInThisMenu(int? MenuId, int? UrunId)
        {
            if (MenuId == null || UrunId == null) throw new Exception("Missing Url Parameter.");
            var selectedMenu = MenuService.GetMenuAndItemsByMenuId(MenuId.Value).First();
            var productListForMenu = selectedMenu.Product.ToList();
            productListForMenu.RemoveAll(x => x.ProductId == UrunId);
            selectedMenu.Product = productListForMenu;
            MenuService.UpdateMenu(selectedMenu);
            return RedirectToAction("UrunListForMenu", new { MenuId = MenuId.Value });
        }
        #endregion
        #region UrunIslemleri
        public ActionResult AllUrunList()
        {
            return View("~/Views/Managements/MenuSection/MenuProcess/AllUrunList.cshtml", MenuService.GetAllItems());
        }
        public ActionResult CreateUrun()
        {
            return View("~/Views/Managements/MenuSection/MenuProcess/CreateUrun.cshtml");
        }
        [HttpPost]
        public ActionResult CreateUrun(ProductProcessModel model)
        {
            if (!ModelState.IsValid) return View(model);
            Product coreDataProduct = new Product
            {
                ProductName = model.ProductName,
            };
            MenuService.InsertItem(coreDataProduct);
            return View("~/Views/Managements/MenuSection/MenuProcess/CreateUrun.cshtml");
        }
        public ActionResult DeleteUrunInAllMenu(int? UrunId)
        {
            if (UrunId == null) throw new Exception("Missing Url Parameter.");
            var SelectedUrunForDelete = MenuService.GetItemsById(UrunId.Value);
            MenuService.DeleteItem(SelectedUrunForDelete);
            return RedirectToAction("AllUrunList");
        }
        #endregion
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            filterContext.ExceptionHandled = true;
        }
    }
}