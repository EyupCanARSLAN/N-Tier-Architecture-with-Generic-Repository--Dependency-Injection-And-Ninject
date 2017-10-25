using Sistem.Core.Data.StatisticK;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
namespace Sistem.Data.Maps.StatisticK
{
    public class MenuSalesStatisticMap : EntityTypeConfiguration<MenuSalesStatistic>
    {
        public MenuSalesStatisticMap()
        {
            HasKey(t => t.MenuSalesStatisticId);
            //ForeignKey MenuId
            HasRequired(t => t.Menu).WithMany(c => c.MenuSalesStatistic).HasForeignKey(t => t.MenuId).WillCascadeOnDelete(false);
            Property(t => t.SalesUniquePrice).HasColumnType("Money")
           .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UniqMonthReport", 1) { IsUnique = true }));
            Property(t => t.SalesMonth)
           .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UniqMonthReport", 2) { IsUnique = true }));
            Property(t => t.SalesYear)
           .HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UniqMonthReport", 3) { IsUnique = true }));
            Property(t => t.MenuId).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("IX_UniqMonthReport", 4) { IsUnique = true }));
        }
    }
}
