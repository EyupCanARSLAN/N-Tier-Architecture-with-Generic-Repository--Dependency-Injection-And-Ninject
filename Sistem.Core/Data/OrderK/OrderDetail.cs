using Sistem.Core.Data.MenuK;
namespace Sistem.Core.Data.OrderK
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int Count { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal UniquePrice { get; set; }
        public int RateOfCustomer { get; set; }
        public int OrderId { get; set; }
        public  Order Order { get; set; }
        public int MenuId { get; set; }
        public  Menu Menu { get; set; }
    }
}
