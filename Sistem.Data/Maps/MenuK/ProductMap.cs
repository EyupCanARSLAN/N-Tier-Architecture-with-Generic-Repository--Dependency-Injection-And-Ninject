using System.Data.Entity.ModelConfiguration;
using Sistem.Core.Data.MenuK;
namespace Sistem.Data.Maps.MenuK
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(t => t.ProductId);
            Property(t => t.ProductName);
        }
    }
}
