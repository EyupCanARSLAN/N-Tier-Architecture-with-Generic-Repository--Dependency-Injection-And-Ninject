using System;
using System.Collections.Generic;
namespace Sistem.Core.Data.OrderK
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public String OrderText { get; set; }
        public int OrderCode { get; set; }
        public  ICollection<Order> Order { get; set; }
    }
}
