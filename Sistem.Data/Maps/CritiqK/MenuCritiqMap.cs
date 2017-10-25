using Sistem.Core.Data.CritiqK;
using System.Data.Entity.ModelConfiguration;
namespace Sistem.Data.Maps.CritiqK
{
    public class MenuCritiqMap : EntityTypeConfiguration<MenuCritiq>
    {
        public MenuCritiqMap()
        {
            HasKey(t => t.CritiqMenuId);
            HasRequired(t => t.Menu).WithMany(c => c.MenuCritiq).HasForeignKey(t => t.MenuId).WillCascadeOnDelete(false);
        }
    }
}
