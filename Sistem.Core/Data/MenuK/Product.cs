using System.Collections.Generic;
namespace Sistem.Core.Data.MenuK
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public  ICollection<Menu> Menu { get; set; }
    }
}
