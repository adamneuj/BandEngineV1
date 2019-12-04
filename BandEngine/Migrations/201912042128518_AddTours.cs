namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTours : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tours",
                c => new
                    {
                        TourId = c.Int(nullable: false, identity: true),
                        ConcertId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TourId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Concerts", t => t.ConcertId, cascadeDelete: false)
                .Index(t => t.ConcertId)
                .Index(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tours", "ConcertId", "dbo.Concerts");
            DropForeignKey("dbo.Tours", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Tours", new[] { "ArtistId" });
            DropIndex("dbo.Tours", new[] { "ConcertId" });
            DropTable("dbo.Tours");
        }
    }
}
