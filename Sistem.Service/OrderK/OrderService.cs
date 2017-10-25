using Sistem.Core.Data.StatisticK;
using Sistem.Core.Data.OrderK;
using Sistem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using Sistem.Service.SystemHelpers;
namespace Sistem.Service.OrderK
{
    public class OrderService : IOrder
    {
        private readonly IRepository<Order> OrderRepository;
        private readonly IRepository<OrderDetail> OrderDetailRepository;
        private readonly IRepository<OrderStatus> OrderStatusRepository;
        private readonly IRepository<MenuSalesStatistic> StatisticRepository;
        public OrderService(IRepository<Order> OrderRepository, IRepository<OrderDetail> OrderDetailRepository, IRepository<OrderStatus> OrderStatusRepository, IRepository<MenuSalesStatistic> StatisticRepository)
        {
            this.OrderRepository = OrderRepository;
            this.OrderDetailRepository = OrderDetailRepository;
            this.OrderStatusRepository = OrderStatusRepository;
            this.StatisticRepository = StatisticRepository;
            InitialValueForOrderStatus();
        }
        #region Order
        public IQueryable<Order> GetAllOrders()
        {
            return OrderRepository.Table;
        }
        public IEnumerable<Order> GetAllOrderByStatusCode(Enums.StatusCode setStatusCode)
        {
            return OrderRepository.Get(filter: b => b.OrderStatus.OrderCode == (int)setStatusCode, orderBy: q => q.OrderByDescending(d => d.Date), includeProperties: "OrderStatus");
        }
        /// <summary>
        /// Show previous order for given customerId
        /// </summary>
        public IEnumerable<Order> GetAllCustomerOrderByCustomerId(String setCustomerId)
        {
            return OrderRepository.Get(filter: b => b.CustomerId == setCustomerId, orderBy: q => q.OrderByDescending(d => d.Date), includeProperties: "OrderDetail,OrderStatus");
        }
        /// <summary>
        /// Return order,orderdetail, customer ıdeas and customer request.
        /// </summary>
        public Order GetOneOrderAndDetail(String setCustomerId, int setOrderId)
        {
            return OrderRepository.Get(filter: b => b.CustomerId == setCustomerId & b.OrderId == setOrderId, orderBy: q => q.OrderByDescending(d => d.Date), includeProperties: "OrderDetail,OrderStatus,OrderDetail.Menu,CritiqOrder").First();
        }
        public Order GetOrderById(int setOrderId)
        {
            return OrderRepository.GetById(setOrderId);
        }
        public void InsertOrder(Order setOrderObj)
        {
            OrderRepository.Insert(setOrderObj);
        }
        public void UpdateOrder(Order setOrderObj)
        {
            OrderRepository.Update(setOrderObj);
        }
        /// <summary>
        /// Close order request and update or create statistic for this sales. Statistic creates according to UniqueKey(Month,Year,Price,MenuId) parameters  
        /// </summary>
        /// <param name="setOrderObj"></param>
        public void CloseOrder(Order setOrderObj)
        {
            UpdateOrder(setOrderObj);
            ManageStatisticProgress(setOrderObj); 
        }
        public void DeleteOrder(Order setOrderObj)
        {
            OrderRepository.Delete(setOrderObj);
        }
        public FavouriteForCustomer CustomerFavoriteMenu(string currentUserId)
        {
            var allOrderDetailsForCustomer = GetAllOrderDetailsByCustomerId(currentUserId).Where(b => b.Order.OrderStatus.OrderCode == (byte)Enums.StatusCode.Complated);
            var maxOrderCountForEachMenuByForCustomer = allOrderDetailsForCustomer.GroupBy(x => x.MenuId)
                .Select(x => new
                {
                    MenuId = x.Key,
                    OrderCount = x.Sum(z => z.Count)
                });
            var statisticResult = maxOrderCountForEachMenuByForCustomer.OrderByDescending(u => u.OrderCount).FirstOrDefault();
            FavouriteForCustomer result = new FavouriteForCustomer();
            result.MenuId = statisticResult.MenuId;
            result.OrderCount = statisticResult.OrderCount;
            return result;
        }
        public void voteProgressForOrder(string currentUserId, int? orderId, int? orderDetailId, int? menuId, int? vote)
        {
            var allSiprarisForCustomer = GetOneOrderAndDetail(currentUserId, orderId.Value);
            var votedOrderDetail = allSiprarisForCustomer.OrderDetail.FirstOrDefault(b => b.OrderId == orderId.Value && b.OrderDetailId == orderDetailId.Value && b.MenuId == menuId.Value && b.RateOfCustomer == 0 && b.Order.CustomerId == currentUserId);
            if (votedOrderDetail != null)
            {
                votedOrderDetail.RateOfCustomer = vote.Value;
                CustomerRateForOrderDetail(votedOrderDetail);
            }
        }
        #endregion
        #region Order Detail
        public IQueryable<OrderDetail> GetAllOrderDetail()
        {
            return OrderDetailRepository.Table;
        }
        public IEnumerable<OrderDetail> GetAllOrderDetailsByCustomerId(String setCustomerId)
        {
           return OrderDetailRepository.Get(filter: b => b.Order.CustomerId == setCustomerId, includeProperties: "Order,Order.OrderStatus");
        }
        public OrderDetail GetOrderDetailById(int setOrderDetailId)
        {
            return OrderDetailRepository.GetById(setOrderDetailId);
        }
        public void InsertOrderDetail(OrderDetail setObj)
        {
            OrderDetailRepository.Insert(setObj);
        }
        public void UpdateOrderDetail(OrderDetail setObj)
        {
            OrderDetailRepository.Update(setObj);
        }
        public void CustomerRateForOrderDetail(OrderDetail orderDetail)
        {
            var IstatistikResult = StatisticRepository.Get(filter: t => t.SalesMonth == orderDetail.Order.Date.Month & t.SalesYear == orderDetail.Order.Date.Year & t.MenuId == orderDetail.MenuId & t.SalesUniquePrice == orderDetail.UniquePrice).FirstOrDefault();
            if (IstatistikResult != null)
            {
                #region Generate New Istatistik Info and Add Table
                IstatistikResult.Vote += orderDetail.RateOfCustomer;
                IstatistikResult.CustomerCountThatGiveVote += 1;
                StatisticRepository.Update(IstatistikResult);
                #endregion
            }
        }
        public void DeleteOrderDetail(OrderDetail setObj)
        {
            OrderDetailRepository.Delete(setObj);
        }
        #endregion
        #region Order Status
        protected void InitialValueForOrderStatus()
        {
            if (GetAllOrderStatus().ToList().Count != 0) return;
            CreateNewStatus("Waiting For CellPhone SMS Code", Enums.StatusCode.WaitPhoneCodeAccept);
            CreateNewStatus("Waiting Restaurant Confurmation", Enums.StatusCode.WaitForProgressFromRst);
            CreateNewStatus("Order Send For Customer", Enums.StatusCode.Complated);
            CreateNewStatus("Order was cancelled by Restaurant", Enums.StatusCode.CancelledByRestaurant);
        }
        private void CreateNewStatus(string statusText,Enums.StatusCode setStatusCode)
        {
            OrderStatus newOrderStatus = new OrderStatus();
            newOrderStatus.OrderText = statusText;
            newOrderStatus.OrderCode = (int)setStatusCode;
            InsertOrderStatus(newOrderStatus);
        }
        public IQueryable<OrderStatus> GetAllOrderStatus()
        {
            return OrderStatusRepository.Table;
        }
        public OrderStatus GetOrderStatusId(int setOrderStatusId)
        {
            return OrderStatusRepository.GetById(setOrderStatusId);
        }
        public OrderStatus GetOrderIdByStatusCode(int statusCode)
        {
            return OrderStatusRepository.Get(filter: t => t.OrderCode == statusCode).First();
        }
        public void InsertOrderStatus(OrderStatus setOrderStatus)
        {
            OrderStatusRepository.Insert(setOrderStatus);
        }
        public void UpdateOrderStatus(OrderStatus setOrderStatus)
        {
            OrderStatusRepository.Update(setOrderStatus);
        }
        public void DeleteOrderStatus(OrderStatus setOrderStatus)
        {
            OrderStatusRepository.Delete(setOrderStatus);
        }
        #endregion
        #region Private Modules
        private void ManageStatisticProgress(Order setStatisticObj)
        {
            foreach (var eachMenu in setStatisticObj.OrderDetail)
            {
                var statisticResult = StatisticRepository.Get(filter: t => t.SalesMonth == setStatisticObj.Date.Month & t.SalesYear == setStatisticObj.Date.Year & t.MenuId == eachMenu.MenuId & t.SalesUniquePrice == eachMenu.UniquePrice).FirstOrDefault();
                if (statisticResult == null) createSalesStatistic(setStatisticObj, eachMenu);
                updateExistingStatistic(statisticResult, eachMenu);
            }
        }
        private void createSalesStatistic(Order setOrderObj,OrderDetail setOrderDetailObj)
        {
            MenuSalesStatistic addNewStatistic = new MenuSalesStatistic();
            addNewStatistic.MenuId = setOrderDetailObj.MenuId;
            addNewStatistic.Vote = 0;
            addNewStatistic.SalesCount = setOrderDetailObj.Count;
            addNewStatistic.SalesYear = setOrderObj.Date.Year;
            addNewStatistic.SalesMonth = setOrderObj.Date.Month;
            addNewStatistic.SalesUniquePrice = setOrderDetailObj.UniquePrice;
            StatisticRepository.Insert(addNewStatistic);
        }
        private void updateExistingStatistic(MenuSalesStatistic setStatistic, OrderDetail setOrderDetailObj)
        {
            setStatistic.SalesCount += setOrderDetailObj.Count;
            StatisticRepository.Update(setStatistic);
        }
        #endregion
    }
}