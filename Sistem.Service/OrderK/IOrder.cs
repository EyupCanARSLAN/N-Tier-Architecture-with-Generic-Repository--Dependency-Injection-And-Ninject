using System;
using System.Collections.Generic;
using System.Linq;
using Sistem.Core.Data.OrderK;
using static Sistem.Service.SystemHelpers.Enums;
using Sistem.Service.SystemHelpers;

namespace Sistem.Service.OrderK
{
    public interface IOrder
    {
        #region Orders/Order
        IQueryable<Order> GetAllOrders();
        /// <summary>
        /// Show previous order for given customerId
        /// </summary>
        IEnumerable<Order> GetAllCustomerOrderByCustomerId(String setCustomerId);
        IEnumerable<Order> GetAllOrderByStatusCode(StatusCode setStatusCode);
        /// <summary>
        /// Return order,orderdetail, customer ıdeas and customer request.
        /// </summary>
        Order GetOneOrderAndDetail(String setCustomerId, int setOrderId);
        Order GetOrderById(int setOrderId);
        FavouriteForCustomer CustomerFavoriteMenu(string currentUserId);
        void CloseOrder(Order setOrderObj);
        void InsertOrder(Order setOrderObj);
        void UpdateOrder(Order setOrderObj);
        void DeleteOrder(Order setOrderObj);
        void voteProgressForOrder(string currentUserId, int? orderId, int? orderDetailId, int? menuId, int? vote);
        #endregion
        #region Order/OrderDetail
        IQueryable<OrderDetail> GetAllOrderDetail();
        IEnumerable<OrderDetail> GetAllOrderDetailsByCustomerId(String setCustomerId);
        OrderDetail GetOrderDetailById(int setOrderDetailId);
        void CustomerRateForOrderDetail(OrderDetail orderDetail);
        void InsertOrderDetail(OrderDetail setOrderDetailObj);
        void UpdateOrderDetail(OrderDetail setOrderDetailObj);
        void DeleteOrderDetail(OrderDetail setOrderDetailObj);
        #endregion
        #region Order/OrderStatus
        IQueryable<OrderStatus> GetAllOrderStatus();
        OrderStatus GetOrderStatusId(int setOrderStatusId);
        OrderStatus GetOrderIdByStatusCode(int statusCode);
        void InsertOrderStatus(OrderStatus setOrderStatus);
        void UpdateOrderStatus(OrderStatus setOrderStatus);
        void DeleteOrderStatus(OrderStatus setOrderStatus);
        #endregion
    }
}