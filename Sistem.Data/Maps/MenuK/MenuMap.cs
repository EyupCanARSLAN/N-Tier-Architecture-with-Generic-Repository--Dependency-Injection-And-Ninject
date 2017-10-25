using System.Data.Entity.ModelConfiguration;
namespace Sistem.Data.Maps.MenuK
{
    public class MenuMap : EntityTypeConfiguration<Core.Data.MenuK.Menu>
    {
        public MenuMap()
        {
            HasKey(t => t.MenuId);
            Property(t => t.Price).HasColumnType("Money");
            HasMany(m => m.Product).WithMany(b => b.Menu)
                                 .Map(t => t.ToTable("Menu_Urun")
                                     .MapLeftKey("MenuId")
                                     .MapRightKey("ProductId"));
        }
    }
}
