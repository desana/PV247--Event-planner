namespace EventPlanner.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoteSessions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeAtPlaces", "EventId", "dbo.Events");
            DropIndex("dbo.TimeAtPlaces", new[] { "EventId" });
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Guid(nullable: false, identity: true),
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
                        EventId = c.Int(nullable: false),
                        VoterName = c.String(),
                        LastModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VoteSessionId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);

            AddColumn("dbo.Places", "EventId", c => c.Int());
            Sql($"UPDATE dbo.Places SET EventId = (SELECT TOP 1 Id FROM dbo.Events)");
            AlterColumn("dbo.Places", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.Places", "EventId");
            AddForeignKey("dbo.Places", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            DropColumn("dbo.TimeAtPlaces", "Votes");
            DropColumn("dbo.TimeAtPlaces", "EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeAtPlaces", "EventId", c => c.Int(nullable: false));
            AddColumn("dbo.TimeAtPlaces", "Votes", c => c.Int(nullable: false));
            DropForeignKey("dbo.Votes", "VoteSession_VoteSessionId", "dbo.VoteSessions");
            DropForeignKey("dbo.VoteSessions", "EventId", "dbo.Events");
            DropForeignKey("dbo.Votes", "TimeAtPlaceId", "dbo.TimeAtPlaces");
            DropForeignKey("dbo.Places", "EventId", "dbo.Events");
            DropIndex("dbo.VoteSessions", new[] { "EventId" });
            DropIndex("dbo.Votes", new[] { "VoteSession_VoteSessionId" });
            DropIndex("dbo.Votes", new[] { "TimeAtPlaceId" });
            DropIndex("dbo.Places", new[] { "EventId" });
            DropColumn("dbo.Places", "EventId");
            DropTable("dbo.VoteSessions");
            DropTable("dbo.Votes");
            CreateIndex("dbo.TimeAtPlaces", "EventId");
            AddForeignKey("dbo.TimeAtPlaces", "EventId", "dbo.Events", "Id", cascadeDelete: true);
        }
    }
}
