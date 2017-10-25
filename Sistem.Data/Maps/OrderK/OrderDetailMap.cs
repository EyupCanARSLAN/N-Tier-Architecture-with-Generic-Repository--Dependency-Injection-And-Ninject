using System.Data.Entity.ModelConfiguration;
namespace Sistem.Data.Maps.OrderK
{
    public class OrderDetailMap : EntityTypeConfiguration<Core.Data.OrderK.OrderDetail>
    {
        public OrderDetailMap()
        {
            HasKey(t => t.OrderDetailId);
            HasRequired(t => t.Order).WithMany(c => c.OrderDetail).HasForeignKey(t => t.OrderId).WillCascadeOnDelete(false);
            HasRequired(t => t.Menu).WithMany(c => c.OrderDetail).HasForeignKey(t => t.MenuId).WillCascadeOnDelete(false);
        }
    }
}
