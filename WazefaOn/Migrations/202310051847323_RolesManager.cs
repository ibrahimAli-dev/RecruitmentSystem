namespace WazefaOn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesManager : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "JobTitle", c => c.String(nullable: false));
            AddColumn("dbo.Categories", "JobContent", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "JobContent");
            DropColumn("dbo.Categories", "JobTitle");
        }
    }
}
