namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeVenueCapacityAndAttendanceNullableType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Concerts", "VenueCapacity", c => c.Int());
            AlterColumn("dbo.Concerts", "Attendance", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Concerts", "Attendance", c => c.Int(nullable: false));
            AlterColumn("dbo.Concerts", "VenueCapacity", c => c.Int(nullable: false));
        }
    }
}
