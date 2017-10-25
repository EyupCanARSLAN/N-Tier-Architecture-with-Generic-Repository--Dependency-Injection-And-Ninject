using Sistem.Core.Data.CritiqK;
using Sistem.Core.Data.StatisticK;
using Sistem.Core.Data.OrderK;
using System.Collections.Generic;
namespace Sistem.Core.Data.MenuK
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public decimal Price { get; set; }
        public  ICollection<OrderDetail> OrderDetail { get; set; }
        public  ICollection<Product> Product { get; set; }
        public  ICollection<MenuCritiq> MenuCritiq { get; set; }
        public  ICollection<MenuSalesStatistic> MenuSalesStatistic { get; set; }
    }
}
