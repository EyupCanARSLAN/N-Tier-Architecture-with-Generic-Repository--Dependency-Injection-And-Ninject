using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
namespace Sistem.Data.Maps.OrderK
{
    public class OrderStatusMap : EntityTypeConfiguration<Core.Data.OrderK.OrderStatus>
    {
        public OrderStatusMap()
        {
            HasKey(t => t.OrderStatusId);
            Property(t => t.OrderCode).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UniqDurumKod", 1) { IsUnique = true }));
        }
    }
}
