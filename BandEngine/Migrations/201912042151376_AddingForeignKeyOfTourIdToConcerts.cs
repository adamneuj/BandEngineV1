namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingForeignKeyOfTourIdToConcerts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tours", "ConcertId", "dbo.Concerts");
            DropIndex("dbo.Tours", new[] { "ConcertId" });
            AddColumn("dbo.Concerts", "TourId", c => c.Int());
            CreateIndex("dbo.Concerts", "TourId");
            AddForeignKey("dbo.Concerts", "TourId", "dbo.Tours", "TourId");
            DropColumn("dbo.Tours", "ConcertId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "ConcertId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Concerts", "TourId", "dbo.Tours");
            DropIndex("dbo.Concerts", new[] { "TourId" });
            DropColumn("dbo.Concerts", "TourId");
            CreateIndex("dbo.Tours", "ConcertId");
            AddForeignKey("dbo.Tours", "ConcertId", "dbo.Concerts", "ConcertId", cascadeDelete: true);
        }
    }
}
