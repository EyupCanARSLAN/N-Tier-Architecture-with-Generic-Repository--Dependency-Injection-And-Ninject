using Sistem.Core.Data.CritiqK;
using System.Data.Entity.ModelConfiguration;
namespace Sistem.Data.Maps.CritiqK
{
    public class CritiqOrderMap : EntityTypeConfiguration<CritiqOrder>
    {
        public CritiqOrderMap()
        {
            HasKey(t => t.CritiqOrderId);
        }
    }
}