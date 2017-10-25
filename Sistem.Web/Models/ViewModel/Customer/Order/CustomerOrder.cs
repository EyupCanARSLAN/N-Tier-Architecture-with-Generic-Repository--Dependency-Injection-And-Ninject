namespace Sistem.Web.Models.ViewModel.Customer.Order
{
    public class CustomerOrder
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int Quantity { get; set; }
        public decimal UniquePrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}