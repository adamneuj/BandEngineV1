namespace BandEngine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(),
                        AddressLine2 = c.String(),
                        City = c.String(),
                        State = c.Int(nullable: false),
                        ZipCode = c.Int(nullable: false),
                        Lat = c.Decimal(precision: 18, scale: 2),
                        Lng = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        AlbumName = c.String(),
                        UPC = c.Int(nullable: false),
                        Progress = c.String(),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AddressId = c.Int(nullable: false),
                        AssociatedAct = c.String(),
                        Genre = c.String(),
                        ApplicationId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ArtistId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationId)
                .Index(t => t.AddressId)
                .Index(t => t.ApplicationId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ArtistTasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Category = c.String(),
                        Progress = c.String(),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Concerts",
                c => new
                    {
                        ConcertId = c.Int(nullable: false, identity: true),
                        ConcertDate = c.DateTime(nullable: false),
                        VenueCapacity = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConcertId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: false)
                .Index(t => t.AddressId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        AddressId = c.Int(nullable: false),
                        PhoneNumber = c.Int(nullable: false),
                        EmailId = c.Int(nullable: false),
                        WebSite = c.String(),
                        Company = c.String(),
                        Role = c.String(),
                        LastContact = c.DateTime(nullable: false),
                        NextContact = c.DateTime(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: false)
                .ForeignKey("dbo.Emails", t => t.EmailId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.EmailId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Int(nullable: false, identity: true),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.EmailId);
            
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ConversationId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Details = c.String(),
                        ContactId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConversationId)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.MailingLists",
                c => new
                    {
                        MailingListId = c.Int(nullable: false, identity: true),
                        EmailId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MailingListId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Emails", t => t.EmailId, cascadeDelete: true)
                .Index(t => t.EmailId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SetLists",
                c => new
                    {
                        SetListId = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        SongId = c.Int(nullable: false),
                        ConcertId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SetListId)
                .ForeignKey("dbo.Concerts", t => t.ConcertId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.ConcertId);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongId = c.Int(nullable: false, identity: true),
                        SongName = c.String(),
                        Genre = c.String(),
                        Progress = c.String(),
                        AlbumId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SongId)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: false)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: false)
                .Index(t => t.AlbumId)
                .Index(t => t.ArtistId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SetLists", "SongId", "dbo.Songs");
            DropForeignKey("dbo.Songs", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Songs", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.SetLists", "ConcertId", "dbo.Concerts");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MailingLists", "EmailId", "dbo.Emails");
            DropForeignKey("dbo.MailingLists", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Conversations", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Contacts", "EmailId", "dbo.Emails");
            DropForeignKey("dbo.Contacts", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Contacts", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Concerts", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Concerts", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.ArtistTasks", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Albums", "ArtistId", "dbo.Artists");
            DropForeignKey("dbo.Artists", "ApplicationId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Artists", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Songs", new[] { "ArtistId" });
            DropIndex("dbo.Songs", new[] { "AlbumId" });
            DropIndex("dbo.SetLists", new[] { "ConcertId" });
            DropIndex("dbo.SetLists", new[] { "SongId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MailingLists", new[] { "ArtistId" });
            DropIndex("dbo.MailingLists", new[] { "EmailId" });
            DropIndex("dbo.Conversations", new[] { "ContactId" });
            DropIndex("dbo.Contacts", new[] { "ArtistId" });
            DropIndex("dbo.Contacts", new[] { "EmailId" });
            DropIndex("dbo.Contacts", new[] { "AddressId" });
            DropIndex("dbo.Concerts", new[] { "ArtistId" });
            DropIndex("dbo.Concerts", new[] { "AddressId" });
            DropIndex("dbo.ArtistTasks", new[] { "ArtistId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Artists", new[] { "ApplicationId" });
            DropIndex("dbo.Artists", new[] { "AddressId" });
            DropIndex("dbo.Albums", new[] { "ArtistId" });
            DropTable("dbo.Songs");
            DropTable("dbo.SetLists");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MailingLists");
            DropTable("dbo.Conversations");
            DropTable("dbo.Emails");
            DropTable("dbo.Contacts");
            DropTable("dbo.Concerts");
            DropTable("dbo.ArtistTasks");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
            DropTable("dbo.Addresses");
        }
    }
}
