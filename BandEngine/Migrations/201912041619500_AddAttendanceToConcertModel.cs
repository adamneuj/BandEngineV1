namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendanceToConcertModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Concerts", "Attendance", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Concerts", "Attendance");
        }
    }
}
