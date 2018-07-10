namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addauthorcolumn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Comments", "Author_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Author_Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.Authors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.Authors");
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropColumn("dbo.Comments", "Author_Id");
            DropTable("dbo.Authors");
        }
    }
}
