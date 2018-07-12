namespace Blog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reset1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ConfirmPassword");
            DropColumn("dbo.AspNetUsers", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
            AddColumn("dbo.AspNetUsers", "ConfirmPassword", c => c.String());
        }
    }
}
