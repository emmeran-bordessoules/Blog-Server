namespace Blog.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Blog.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Blog.Models.BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Blog.Models.BlogContext context)
        {
            //context.Users.AddOrUpdate(x => x.Id,
            //    new Author() { UserName = "Turlututu 1", Roles = "user" },
            //    new Author() { UserName = "Turlututu 2", Roles = "Chapeau pointu 2" },
            //    new Author() { UserName = "Turlututu 3", Roles = "Chapeau pointu 3" }
            //);
        }
    }
}
