namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeToList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false, maxLength: 50),
                        EventDescription = c.String(),
                        EventLink = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FourSquareLink = c.String(),
                        Event_EventId = c.Int(),
                    })
                .PrimaryKey(t => t.PlaceId)
                .ForeignKey("dbo.Events", t => t.Event_EventId)
                .Index(t => t.Event_EventId);
            
            CreateTable(
                "dbo.TimeAtPlaces",
                c => new
                    {
                        TimeAtPlaceId = c.Int(nullable: false, identity: true),
                        Event_EventId = c.Int(),
                        Place_PlaceId = c.Int(),
                    })
                .PrimaryKey(t => t.TimeAtPlaceId)
                .ForeignKey("dbo.Events", t => t.Event_EventId)
                .ForeignKey("dbo.Places", t => t.Place_PlaceId)
                .Index(t => t.Event_EventId)
                .Index(t => t.Place_PlaceId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        Votes = c.Int(nullable: false),
                        Event_EventId = c.Int(),
                        TimeAtPlace_TimeAtPlaceId = c.Int(),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.Events", t => t.Event_EventId)
                .ForeignKey("dbo.TimeAtPlaces", t => t.TimeAtPlace_TimeAtPlaceId)
                .Index(t => t.Event_EventId)
                .Index(t => t.TimeAtPlace_TimeAtPlaceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "TimeAtPlace_TimeAtPlaceId", "dbo.TimeAtPlaces");
            DropForeignKey("dbo.Votes", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.TimeAtPlaces", "Place_PlaceId", "dbo.Places");
            DropForeignKey("dbo.TimeAtPlaces", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Places", "Event_EventId", "dbo.Events");
            DropIndex("dbo.Votes", new[] { "TimeAtPlace_TimeAtPlaceId" });
            DropIndex("dbo.Votes", new[] { "Event_EventId" });
            DropIndex("dbo.TimeAtPlaces", new[] { "Place_PlaceId" });
            DropIndex("dbo.TimeAtPlaces", new[] { "Event_EventId" });
            DropIndex("dbo.Places", new[] { "Event_EventId" });
            DropTable("dbo.Votes");
            DropTable("dbo.TimeAtPlaces");
            DropTable("dbo.Places");
            DropTable("dbo.Events");
        }
    }
}
