namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateContactAddWebsite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "WebSite", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "WebSite");
        }
    }
}
