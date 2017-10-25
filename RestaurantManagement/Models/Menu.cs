using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public class Menu
    {
        public Menu()
        {
            Product = new List<Product>();
        }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal Price { get; set; }
        public dynamic OrderDetail { get; set; }
        public List<Product> Product { get; set; }
        public dynamic MenuCritiq { get; set; }
        public dynamic MenuSalesStatistic { get; set; }
    }
    public class Product
    {
        public Product()
        {
            Menu = new List<Menu>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public ICollection<Menu> Menu { get; set; }
    }
}
