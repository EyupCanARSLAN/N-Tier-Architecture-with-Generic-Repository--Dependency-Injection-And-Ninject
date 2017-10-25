namespace Sistem.Data.Migrations
{
    using System.Data.Entity.Migrations;
    internal sealed class Configuration : DbMigrationsConfiguration<SistemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        protected override void Seed(SistemDbContext context)
        {
        }
    }
}
