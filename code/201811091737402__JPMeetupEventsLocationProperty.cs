namespace JobPlacementDashboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _JPMeetupEventsLocationProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.JPMeetupEvents", "JPLocation", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.JPMeetupEvents", "JPLocation");
        }
    }
}
