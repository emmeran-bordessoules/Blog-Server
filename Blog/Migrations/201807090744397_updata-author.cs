namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updataauthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authors", "UserName", c => c.String(nullable: false));
            AddColumn("dbo.Authors", "Password", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Authors", "ConfirmPassword", c => c.String());
            DropColumn("dbo.Authors", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Authors", "Name", c => c.String());
            DropColumn("dbo.Authors", "ConfirmPassword");
            DropColumn("dbo.Authors", "Password");
            DropColumn("dbo.Authors", "UserName");
        }
    }
}
