using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Sistem.Web.Models.ViewModel.Customer.Order;
using System.Net;
using PagedList;
using Sistem.Web.Models.ViewModel.Customer.Feedback;
using Sistem.Core.Data.OrderK;
using Sistem.Service.OrderK;
using Sistem.Service.MenuK;
using Sistem.Service.CritiqK;
using Sistem.Core.Data.CritiqK;
using Sistem.Core.Data.MenuK;
using static Sistem.Service.SystemHelpers.Enums;
namespace Sistem.Web.Controllers.Customer
{
    [Authorize(Roles = "Customer")]
    public class CustomerMenuController : Controller
    {
        private readonly ICritiq districtService;
        private readonly IMenu menuService;
        private readonly IOrder orderService;
        public CustomerMenuController(IOrder IOrderService, IMenu IMenuService, ICritiq IDistrictService)
        {
            orderService = IOrderService;
            menuService = IMenuService;
            districtService = IDistrictService;
        }
        public ActionResult Index(int? page)
        {
            var currentUserId = User.Identity.GetUserId();
            //Show customer general feedback about on restaurant and menus
            var customerFeedBackList = new List<Feedback>();
            #region GenerateDropdown for Customer Feedback
            ViewBag.DropdownForFeedBack = feedBackSubjectList();
            #endregion
            #region Feedback For General Purpose
            //Set "CustomerFeedbackList" with customer general comments
            var feedbackListAsGeneral = getListOfGeneralFeedback(currentUserId);
            customerFeedBackList.AddRange(feedbackListAsGeneral);
            #endregion
            #region feedback for Menu
            //Set "CustomerFeedbackList" with customer  comments about on menu
            var feedbackListForMenu = getListOfMenuFeedback(currentUserId);
            customerFeedBackList.AddRange(feedbackListForMenu);
            #endregion
            if (orderService.GetAllCustomerOrderByCustomerId(currentUserId).Where(b => b.OrderStatus.OrderCode == (byte)StatusCode.Complated).ToList().Count != 0)
            {
                #region Last Order For Customer
                var lastOrderForCustomer = orderService.GetAllCustomerOrderByCustomerId(currentUserId).Where(b => b.OrderStatus.OrderCode == (byte)StatusCode.Complated).FirstOrDefault();
                ViewBag.LastOrder = lastOrderForCustomer;
                #endregion
                #region Customer's Favourite Menu
                var customerFavouriteMenu = orderService.CustomerFavoriteMenu(currentUserId);
                ViewBag.FavouriteMenuOrderCount = customerFavouriteMenu.OrderCount;
                ViewBag.FavouriteMenu = menuService.GetMenuAndItemsByMenuId(customerFavouriteMenu.MenuId).FirstOrDefault();
                #endregion
            }
            int pageSize = 5; int pageNumber = (page ?? 1);
            return View("~/Views/Customers/CustomerMenu/Index.cshtml", customerFeedBackList.OrderByDescending(b => b.Date).ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        ///  Customers old orders and details for these.
        /// </summary>
        public ActionResult HistoryOfOrder(int? page)
        {
            var currentUserId = User.Identity.GetUserId();
            var allSiprarisForCustomer = orderService.GetAllCustomerOrderByCustomerId(currentUserId);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View("~/Views/Customers/CustomerMenu/HistoryOfOrder.cshtml", allSiprarisForCustomer.ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// Prepare Menu for Customer Order.
        /// </summary>
        public ActionResult CreateOrder()
        {
            var menuList = menuService.GetMenuAndItemsByMenuId();
            return View("~/Views/Customers/CustomerMenu/CreateOrder.cshtml", menuList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrder(int? menuId, String menuName, int? orderCount)
        {
            if (menuId == null || menuName.Trim() == String.Empty || orderCount == null) throw new Exception("Missing Url Parameter.");
            var selectedMenu = menuService.GetMenuById(menuId.Value);
            if (selectedMenu == null || selectedMenu.MenuName != menuName) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            #region AddNewOrder in OrderList- Sepete Ekleme Yapıyor...
            addNewOrderToBucket(selectedMenu,orderCount);
            #endregion
            return RedirectToAction("CreateOrder");
        }
        [HttpGet]
        public ActionResult ComplateOrder()
        {
            if (Session["CustomerOrderList"] == null) return RedirectToAction("HistoryOfOrder");
            var orderRecord = createOrderOnDb();
            saveOrderOnDb(orderRecord);
            Session["CustomerOrderList"] = null;
            return RedirectToAction("HistoryOfOrder");
        }
        /// <summary>
        /// Customer can remove item in OrderList.
        /// </summary>
        [HttpGet]
        public ActionResult DeleteOrder(int? menuId, String menuName)
        {
            if (menuId == null || menuName.Trim() == String.Empty || Session["CustomerOrderList"] == null) throw new Exception("Missing Url Parameter.");
            deleteOrderInList(menuId.Value, menuName);
            return RedirectToAction("CreateOrder");
        }
        [HttpGet]
        public ActionResult DetailsForCustomerOrder(int? orderId)
        {
            if (orderId == null) throw new Exception("Missing Url Parameter.");
            var currentUserId = User.Identity.GetUserId();
            var allSiprarisForCustomer = orderService.GetOneOrderAndDetail(currentUserId, orderId.Value);
            return View("~/Views/Customers/CustomerMenu/DetailsForCustomerOrder.cshtml", allSiprarisForCustomer);
        }
        /// <summary>
        /// Customer Vote progress is executed at there.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="detailNo"></param>
        /// <param name="menuId"></param>
        /// <param name="vote"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DetailsForCustomerOrder(int? orderId, int? detailNo, int? menuId, int? vote)
        {
            if (orderId == null || detailNo == null || menuId == null || vote == null || vote.Value <= 0 || vote.Value > 5) throw new Exception("Missing Url Parameter.");
            var currentUserId = User.Identity.GetUserId();
            orderService.voteProgressForOrder(currentUserId, orderId, detailNo, menuId, vote);
            return RedirectToAction("DetailsForCustomerOrder", new { SiparisId = orderId.Value });
        }
        /// <summary>
        /// Set customer comment about on order.
        /// </summary>
        [HttpPost]
        public ActionResult CustomerComment(int? orderId, String customerComment)
        {
            if (orderId == null || customerComment.Trim() == String.Empty) throw new Exception("Missing Url Parameter.");
            var currentUserId = User.Identity.GetUserId();
            var allOrderForCustomer = orderService.GetOneOrderAndDetail(currentUserId, orderId.Value);
            var customerElestiri = new CritiqOrder();
            customerElestiri.CritiqMessage = customerComment;
            allOrderForCustomer.CritiqOrder.Add(customerElestiri);
            orderService.UpdateOrder(allOrderForCustomer);
            return RedirectToAction("DetailsForCustomerOrder", new { SiparisId = orderId.Value });
        }
        /// <summary>
        /// Set customer comment about on menu and general ideas.
        /// </summary>
        [HttpPost]
        public ActionResult CommentForMenuOrGeneral(String customerFeedback, String dropdownForFeedBack)
        {
            if (customerFeedback.Trim() == String.Empty || dropdownForFeedBack.Trim() == String.Empty) throw new Exception("Missing Url Parameter.");
            var currentUserId = User.Identity.GetUserId();
            saveCustomerComment(dropdownForFeedBack, customerFeedback, currentUserId);
            return RedirectToAction("Index");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            filterContext.ExceptionHandled = true;
        }
        #region Private Methods
        private Feedback createFeedbackObj(string setAnswerText, string setCriticText, bool setIsSeen, string setSubject, DateTime setDate)
        {
            var customerFeedBack = new Feedback();
            customerFeedBack.Answer = setAnswerText;
            customerFeedBack.CritiqText = setCriticText;
            customerFeedBack.IsSeen = setIsSeen;
            customerFeedBack.Subject = setSubject;
            customerFeedBack.Date = setDate;
            return customerFeedBack;
        }
        /// <summary>
        /// Convert DbModel to ViewModel
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        private List<Feedback> getListOfGeneralFeedback(string currentUserId)
        {
            var resultList = new List<Feedback>();
            var feedBackList = districtService.GetObjForCommonByCustId(currentUserId).OrderByDescending(q => q.GeneralCritiqId);
            foreach (var eachItem in feedBackList)
            {
                var feedback = createFeedbackObj(eachItem.AnswerText, eachItem.CritiqText, eachItem.IsSeen, "General Feedback", eachItem.Date);
                resultList.Add(feedback);
            }
            return resultList;
        }
        /// <summary>
        /// Convert DbModel to ViewModel
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns></returns>
        private List<Feedback> getListOfMenuFeedback(string currentUserId)
        {
            var resultList = new List<Feedback>();
            var feedbackListForMenu = districtService.GetListForMenuByCustomerId(currentUserId).OrderByDescending(q => q.CritiqMenuId);
            foreach (var eachItem in feedbackListForMenu)
            {
                var feedback = createFeedbackObj(eachItem.Answer, eachItem.CritiqMessage, eachItem.IsSeen, eachItem.Menu.MenuName, eachItem.Date);
                resultList.Add(feedback);
            }
            return resultList;
        }
        private List<SelectListItem> feedBackSubjectList()
        {
            var feedBackSubject = new List<SelectListItem>();
            feedBackSubject.Add(new SelectListItem { Text = "General Subject", Value = "GeneralInformation", Selected = true });
            foreach (var eachMenu in menuService.GetAllItem())
                feedBackSubject.Add(new SelectListItem { Text = eachMenu.MenuName, Value = eachMenu.MenuId.ToString() });
            return feedBackSubject;
        }
        private void addNewOrderToBucket(Menu selectedMenu, int? orderCount)
        {
            var orderListForCustomer = new LinkedList<CustomerOrder>();
            if (Session["CustomerOrderList"] != null) orderListForCustomer = (LinkedList<CustomerOrder>)Session["CustomerOrderList"];
            var newOrder = new CustomerOrder();
            newOrder.MenuId = selectedMenu.MenuId;
            newOrder.MenuName = selectedMenu.MenuName;
            newOrder.Quantity = orderCount.Value;
            newOrder.UniquePrice = selectedMenu.Price;
            newOrder.TotalPrice = selectedMenu.Price * orderCount.Value;
            orderListForCustomer.AddLast(newOrder);
            Session["CustomerOrderList"] = orderListForCustomer;
        }
        private void deleteOrderInList(int menuId, string menuName)
        {
            #region Delete Order in OrderList
            var orderListForCustomer = new LinkedList<CustomerOrder>();
            orderListForCustomer = (LinkedList<CustomerOrder>)Session["CustomerOrderList"];
            var deletedOrder = orderListForCustomer.Where(o => o.MenuId == menuId && o.MenuName == menuName).First();
            if (deletedOrder != null) orderListForCustomer.Remove(deletedOrder);
            Session["CustomerOrderList"] = orderListForCustomer;
            #endregion
        }
        /// <summary>
        /// Create new order and details request with Order Bucket.
        /// </summary>
        /// <returns></returns>
        private Order createOrderOnDb()
        {
            var customerOrder = new Order();
            customerOrder.CustomerId = User.Identity.GetUserId();
            customerOrder.Date = DateTime.Now;
            customerOrder.OrderStatus = orderService.GetOrderIdByStatusCode((byte)StatusCode.WaitForProgressFromRst);
            customerOrder.OrderStatus.OrderStatusId = customerOrder.OrderStatus.OrderStatusId;
            orderService.InsertOrder(customerOrder);
            return customerOrder;
        }
        private void saveOrderOnDb(Order orderRecord)
        {
            decimal TotalPrice = 0;
            var orderListForCustomer = new LinkedList<CustomerOrder>();
            orderListForCustomer = (LinkedList<CustomerOrder>)Session["CustomerOrderList"];
            foreach (var eachOrder in orderListForCustomer)
            {
                var customerSiparisDetay = new OrderDetail();
                customerSiparisDetay.MenuId = eachOrder.MenuId;
                customerSiparisDetay.OrderId = orderRecord.OrderId;
                customerSiparisDetay.RateOfCustomer = 0;
                customerSiparisDetay.Count = eachOrder.Quantity;
                customerSiparisDetay.SalesPrice = eachOrder.TotalPrice;
                customerSiparisDetay.UniquePrice = eachOrder.UniquePrice;
                orderService.InsertOrderDetail(customerSiparisDetay);
                TotalPrice += eachOrder.TotalPrice;
            }
            orderRecord.TotalPrice = TotalPrice;
            orderRecord.PhoneCodeAccept = false;
            orderRecord.CloseThisOrder = false;
            orderService.UpdateOrder(orderRecord);
        }
        private void saveCustomerComment(string dropdownForFeedBack, string customerFeedback, string currentUserId)
        {
            if (dropdownForFeedBack == "GeneralInformation")
            {
                var customerElestiri = new GeneralCritiq();
                customerElestiri.CritiqText = customerFeedback;
                customerElestiri.CustomerId = currentUserId;
                customerElestiri.IsSeen = false;
                customerElestiri.IsPublished = false;
                customerElestiri.Date = DateTime.Now;
                districtService.InsertCommon(customerElestiri);
            }
            else
            {
                var customerElestiri = new MenuCritiq();
                customerElestiri.CritiqMessage = customerFeedback;
                customerElestiri.CustomerId = currentUserId;
                customerElestiri.IsSeen = false;
                customerElestiri.Date = DateTime.Now;
                int menuId = Int32.Parse(dropdownForFeedBack);
                var selectedMenuForComment = menuService.GetMenuStatisticByMenuId(menuId).FirstOrDefault();
                selectedMenuForComment.MenuCritiq.Add(customerElestiri);
                menuService.UpdateMenu(selectedMenuForComment);
            }
        }
        #endregion
    }
}