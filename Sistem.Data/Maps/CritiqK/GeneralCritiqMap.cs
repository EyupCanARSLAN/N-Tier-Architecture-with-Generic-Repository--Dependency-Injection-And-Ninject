using System.Data.Entity.ModelConfiguration;
using Sistem.Core.Data.CritiqK;
namespace Sistem.Data.Maps.CritiqK
{
    public class GeneralCritiqMap : EntityTypeConfiguration<GeneralCritiq>
    {
        public GeneralCritiqMap()
        {
            HasKey(t => t.GeneralCritiqId);
        }
    }
}
