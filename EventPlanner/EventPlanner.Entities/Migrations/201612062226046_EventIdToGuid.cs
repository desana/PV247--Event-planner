namespace EventPlanner.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventIdToGuid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Places", "EventId", "dbo.Events");
            DropForeignKey("dbo.VoteSessions", "EventId", "dbo.Events");
            DropIndex("dbo.Places", new[] { "EventId" });
            DropIndex("dbo.VoteSessions", new[] { "EventId" });
            DropPrimaryKey("dbo.Events");
            AlterColumn("dbo.Events", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Places", "EventId", c => c.Guid(nullable: false));
            AlterColumn("dbo.VoteSessions", "EventId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Events", "Id");
            CreateIndex("dbo.Places", "EventId");
            CreateIndex("dbo.VoteSessions", "EventId");
            AddForeignKey("dbo.Places", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VoteSessions", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            DropColumn("dbo.Events", "Link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Link", c => c.String(nullable: false));
            DropForeignKey("dbo.VoteSessions", "EventId", "dbo.Events");
            DropForeignKey("dbo.Places", "EventId", "dbo.Events");
            DropIndex("dbo.VoteSessions", new[] { "EventId" });
            DropIndex("dbo.Places", new[] { "EventId" });
            DropPrimaryKey("dbo.Events");
            AlterColumn("dbo.VoteSessions", "EventId", c => c.Int(nullable: false));
            AlterColumn("dbo.Places", "EventId", c => c.Int(nullable: false));
            AlterColumn("dbo.Events", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Events", "Id");
            CreateIndex("dbo.VoteSessions", "EventId");
            CreateIndex("dbo.Places", "EventId");
            AddForeignKey("dbo.VoteSessions", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Places", "EventId", "dbo.Events", "Id", cascadeDelete: true);
        }
    }
}
