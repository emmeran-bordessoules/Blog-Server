namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkauthorpost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            AddColumn("dbo.Comments", "AuthorName", c => c.String());
            AddColumn("dbo.Posts", "AuthorName", c => c.String());
            DropColumn("dbo.Comments", "Author_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Author_Id", c => c.Int());
            DropColumn("dbo.Posts", "AuthorName");
            DropColumn("dbo.Comments", "AuthorName");
            CreateIndex("dbo.Comments", "Author_Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.Authors", "Id");
        }
    }
}
