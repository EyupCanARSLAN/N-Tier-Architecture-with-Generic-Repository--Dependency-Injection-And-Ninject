using System.Data.Entity.ModelConfiguration;
namespace Sistem.Data.Maps.OrderK
{
    public class OrderMap : EntityTypeConfiguration<Core.Data.OrderK.Order>
    {
        public OrderMap()
        {
            HasKey(t => t.OrderId);
            Property(t => t.TotalPrice).HasColumnType("Money");
        }
    }
}
