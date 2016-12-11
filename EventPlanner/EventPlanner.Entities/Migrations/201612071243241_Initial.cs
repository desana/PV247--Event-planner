namespace EventPlanner.Entities.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EventId = c.Guid(nullable: false),
                        Name = c.String(),
                        FourSquareId = c.String(),
                        FourSquareLink = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.TimeAtPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlaceId = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Places", t => t.PlaceId, cascadeDelete: true)
                .Index(t => t.PlaceId);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Guid(nullable: false),
                        TimeAtPlaceId = c.Int(nullable: false),
                        Value = c.String(),
                        VoteSession_VoteSessionId = c.Guid(),
                    })
                .PrimaryKey(t => t.VoteId)
                .ForeignKey("dbo.TimeAtPlaces", t => t.TimeAtPlaceId, cascadeDelete: true)
                .ForeignKey("dbo.VoteSessions", t => t.VoteSession_VoteSessionId)
                .Index(t => t.TimeAtPlaceId)
                .Index(t => t.VoteSession_VoteSessionId);
            
            CreateTable(
                "dbo.VoteSessions",
                c => new
                    {
                        VoteSessionId = c.Guid(nullable: false, identity: true),
                        EventId = c.Guid(nullable: false),
                        VoterName = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VoteSessionId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "VoteSession_VoteSessionId", "dbo.VoteSessions");
            DropForeignKey("dbo.VoteSessions", "EventId", "dbo.Events");
            DropForeignKey("dbo.Votes", "TimeAtPlaceId", "dbo.TimeAtPlaces");
            DropForeignKey("dbo.TimeAtPlaces", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.Places", "EventId", "dbo.Events");
            DropIndex("dbo.VoteSessions", new[] { "EventId" });
            DropIndex("dbo.Votes", new[] { "VoteSession_VoteSessionId" });
            DropIndex("dbo.Votes", new[] { "TimeAtPlaceId" });
            DropIndex("dbo.TimeAtPlaces", new[] { "PlaceId" });
            DropIndex("dbo.Places", new[] { "EventId" });
            DropTable("dbo.VoteSessions");
            DropTable("dbo.Votes");
            DropTable("dbo.TimeAtPlaces");
            DropTable("dbo.Places");
            DropTable("dbo.Events");
        }
    }
}
