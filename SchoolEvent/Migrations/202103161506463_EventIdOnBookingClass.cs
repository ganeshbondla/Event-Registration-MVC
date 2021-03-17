namespace SchoolEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventIdOnBookingClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserBookings", "Eid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserBookings", "Eid");
        }
    }
}
