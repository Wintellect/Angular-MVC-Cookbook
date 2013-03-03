using MvcAngular.Web.Repository;

namespace MvcAngular.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MvcAngular.Web.Repository.ExampleDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MvcAngular.Web.Repository.ExampleDbContext context)
        {
            SeedData.Seed(context);
        }
    }
}
