namespace WazefaOn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsActiveJob : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "isDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "isDeleted");
            DropColumn("dbo.Jobs", "isActive");
        }
    }
}
