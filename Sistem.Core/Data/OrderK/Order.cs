using Sistem.Core.Data.CritiqK;
using System;
using System.Collections.Generic;
namespace Sistem.Core.Data.OrderK
{
    public class Order
    {
        public int OrderId { get; set; }
        public String CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalPrice { get; set; }
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Boolean CloseThisOrder { get; set; }
        public Boolean PhoneCodeAccept { get; set; }
        public ICollection<CritiqOrder> CritiqOrder { get; set; }
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}