namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedLatLngInAddressToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Addresses", "Lat", c => c.String());
            AlterColumn("dbo.Addresses", "Lng", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Addresses", "Lng", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Addresses", "Lat", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
