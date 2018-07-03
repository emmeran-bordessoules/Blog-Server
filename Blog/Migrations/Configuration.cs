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
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Blog.Models.BlogContext context)
        {
            context.Posts.AddOrUpdate(x => x.Id,
                new Post() { Id = 1, Title = "Turlututu 1", Content = "Chapeau pointu 1", CreationDate = DateTime.UtcNow.AddDays(-1) },
                new Post() { Id = 2, Title = "Turlututu 2", Content = "Chapeau pointu 2", CreationDate = DateTime.UtcNow.AddDays(-2) },
                new Post() { Id = 3, Title = "Turlututu 3", Content = "Chapeau pointu 3", CreationDate = DateTime.UtcNow.AddDays(-3) }
            );

            context.Comments.AddOrUpdate(x => x.Id,
                new Comment() { Id = 1, Content = "Commentaire 1", CreationDate = DateTime.UtcNow.AddDays(-1), PostId = 2 },
                new Comment() { Id = 2, Content = "Commentaire 2", CreationDate = DateTime.UtcNow.AddDays(-2), PostId = 2 },
                new Comment() { Id = 3, Content = "Commentaire 3", CreationDate = DateTime.UtcNow.AddDays(-3), PostId = 3 }
            );
        }
    }
}
