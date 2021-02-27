namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attendances", "Gig_ID", c => c.Int());
            CreateIndex("dbo.Attendances", "Gig_ID");
            AddForeignKey("dbo.Attendances", "Gig_ID", "dbo.Gigs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "Gig_ID", "dbo.Gigs");
            DropIndex("dbo.Attendances", new[] { "Gig_ID" });
            DropColumn("dbo.Attendances", "Gig_ID");
        }
    }
}
