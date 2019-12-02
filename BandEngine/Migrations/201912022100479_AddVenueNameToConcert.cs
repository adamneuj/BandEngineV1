namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVenueNameToConcert : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Concerts", "VenueName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Concerts", "VenueName");
        }
    }
}
