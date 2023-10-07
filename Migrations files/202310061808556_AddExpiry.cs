namespace WazefaOn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExpiry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "ExpiryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ApplyForJobs", "ExpiryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplyForJobs", "ExpiryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Jobs", "ExpiryDate");
        }
    }
}
