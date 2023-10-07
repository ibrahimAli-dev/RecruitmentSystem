namespace WazefaOn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxApplications : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "ExpiryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "MaxApplicants", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "MaxApplicants");
            DropColumn("dbo.ApplyForJobs", "ExpiryDate");
        }
    }
}
