namespace SchoolEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserBookings", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.UserBookings", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.UserBookings", "Mobile", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserBookings", "Mobile", c => c.String());
            AlterColumn("dbo.UserBookings", "Email", c => c.String());
            AlterColumn("dbo.UserBookings", "Name", c => c.String());
        }
    }
}
