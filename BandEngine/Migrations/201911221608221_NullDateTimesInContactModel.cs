namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullDateTimesInContactModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Contacts", "LastContact", c => c.DateTime());
            AlterColumn("dbo.Contacts", "NextContact", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "NextContact", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Contacts", "LastContact", c => c.DateTime(nullable: false));
        }
    }
}
