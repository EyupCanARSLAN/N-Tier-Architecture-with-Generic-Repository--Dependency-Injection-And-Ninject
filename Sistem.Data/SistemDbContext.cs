using System;
using System.Linq;
using System.Data.Entity;
using System.Reflection;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
namespace Sistem.Data
{
   public class SistemDbContext: DbContext,IDbContext
    {
        public SistemDbContext() : base("name=DbConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = false;
            //Migrations
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<SistemDbContext, Migrations.Configuration>("DbConnectionString"));
          //  Database.SetInitializer<SistemDbContext>(new CreateDatabaseIfNotExists<SistemDbContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
       .Where(type => !String.IsNullOrEmpty(type.Namespace))
       .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
       type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }
            base.OnModelCreating(modelBuilder);
        }
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
    }
}