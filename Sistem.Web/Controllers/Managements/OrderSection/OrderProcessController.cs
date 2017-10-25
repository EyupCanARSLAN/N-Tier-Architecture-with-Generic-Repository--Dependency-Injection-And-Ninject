using Sistem.Service.OrderK;
using Sistem.Service.SystemHelpers;
using System;
using System.Web.Mvc;
namespace Sistem.Web.Controllers.Managements.OrderSection
{
    /// <summary>
    /// At this controller; Manager can manage Customer Orders
    /// </summary>
    [Authorize(Roles = "Managements")]
    public class OrderProcessController : Controller
    {
        private readonly IOrder OrderService;
        public OrderProcessController(IOrder ISiparisService)
        {
            OrderService = ISiparisService;
        }
        public ActionResult Index()
        {
            var NewOrders = OrderService.GetAllOrderByStatusCode(Enums.StatusCode.WaitForProgressFromRst);
            return View("~/Views/Managements/OrderProcess/Index.cshtml", NewOrders);
        }
        [HttpGet]
        public ActionResult MoveOnThisOrder(int? SiparisId, String MusteriId)
        {
            if (SiparisId == null || MusteriId.Trim() == String.Empty) throw new Exception("Missing Url Parameter.");
            var newOrders = OrderService.GetOneOrderAndDetail(MusteriId, SiparisId.Value);
            if (newOrders == null) throw new Exception("New Orders Object is null.");
            closeOrder(MusteriId, SiparisId.Value, Enums.StatusCode.Complated);
            return View("~/Views/Managements/OrderProcess/MoveOnThisOrder.cshtml", newOrders);
        }
        [HttpGet]
        public ActionResult CloseOrder(int? SiparisId, String MusteriId)
        {
            if (SiparisId == null || MusteriId.Trim() == String.Empty) throw new Exception("Missing Url Parameter.");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CancelOrder(int? SiparisId, String MusteriId)
        {
            if (SiparisId == null || MusteriId.Trim() == String.Empty) throw new Exception("Missing Url Parameter.");
            closeOrder(MusteriId, SiparisId.Value, Enums.StatusCode.CancelledByRestaurant);
            return RedirectToAction("Index");
        }
        protected override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            filterContext.ExceptionHandled = true;
        }
        #region Private Methods
        private void closeOrder(string customerId, int orderId, Enums.StatusCode status)
        {
            var closedOrder = OrderService.GetOneOrderAndDetail(customerId, orderId);
            if (closedOrder == null) return;
            closedOrder.OrderStatus = OrderService.GetOrderIdByStatusCode((byte)status);
            closedOrder.OrderStatusId = closedOrder.OrderStatus.OrderStatusId;
            closedOrder.CloseThisOrder = true;
            if (status == Enums.StatusCode.Complated) OrderService.CloseOrder(closedOrder);
            if (status == Enums.StatusCode.CancelledByRestaurant) OrderService.UpdateOrder(closedOrder);
        }
        #endregion
    }
}