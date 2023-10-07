namespace WazefaOn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Activated : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Jobs", "JobImage");
            DropColumn("dbo.Jobs", "isDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "isDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "JobImage", c => c.String());
        }
    }
}
