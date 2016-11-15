namespace EventPlanner.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseRefactoring : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Places", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Votes", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.Votes", "TimeAtPlace_TimeAtPlaceId", "dbo.TimeAtPlaces");
            DropForeignKey("dbo.TimeAtPlaces", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.TimeAtPlaces", "Place_PlaceId", "dbo.Places");

            DropIndex("dbo.Places", new[] { "Event_EventId" });
            DropIndex("dbo.Votes", new[] { "Event_EventId" });
            DropIndex("dbo.Votes", new[] { "TimeAtPlace_TimeAtPlaceId" });

            RenameColumn(table: "dbo.TimeAtPlaces", name: "Event_EventId", newName: "Event_Id");
            RenameColumn(table: "dbo.TimeAtPlaces", name: "Place_PlaceId", newName: "Place_Id");
            RenameIndex(table: "dbo.TimeAtPlaces", name: "IX_Event_EventId", newName: "IX_Event_Id");
            RenameIndex(table: "dbo.TimeAtPlaces", name: "IX_Place_PlaceId", newName: "IX_Place_Id");

            DropPrimaryKey("dbo.Events");
            DropPrimaryKey("dbo.Places");
            DropPrimaryKey("dbo.TimeAtPlaces");

            AddColumn("dbo.Events", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Events", "Description", c => c.String());
            AddColumn("dbo.Events", "Link", c => c.String(nullable: false));
            AddColumn("dbo.Places", "FourSquareId", c => c.String());
            AddColumn("dbo.TimeAtPlaces", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.TimeAtPlaces", "Votes", c => c.Int(nullable: false));
            
            DropColumn("dbo.Events", "EventName");
            DropColumn("dbo.Events", "EventDescription");
            DropColumn("dbo.Events", "EventLink");

            DropColumn("dbo.Places", "PlaceId");
            DropColumn("dbo.Events", "EventId");
            DropColumn("dbo.Places", "Event_EventId");
            DropColumn("dbo.TimeAtPlaces", "TimeAtPlaceId");

            AddColumn("dbo.Events", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Places", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.TimeAtPlaces", "Id", c => c.Int(nullable: false, identity: true));

            AddPrimaryKey("dbo.Events", "Id");
            AddPrimaryKey("dbo.Places", "Id");
            AddPrimaryKey("dbo.TimeAtPlaces", "Id");

            AddForeignKey("dbo.TimeAtPlaces", "Event_Id", "dbo.Events", "Id");
            AddForeignKey("dbo.TimeAtPlaces", "Place_Id", "dbo.Places", "Id");

            DropTable("dbo.Votes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        VoteId = c.Int(nullable: false, identity: true),
                        Votes = c.Int(nullable: false),
                        Event_EventId = c.Int(),
                        TimeAtPlace_TimeAtPlaceId = c.Int(),
                    })
                .PrimaryKey(t => t.VoteId);
            
            AddColumn("dbo.TimeAtPlaces", "TimeAtPlaceId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Places", "Event_EventId", c => c.Int());
            AddColumn("dbo.Places", "PlaceId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Events", "EventLink", c => c.String(nullable: false));
            AddColumn("dbo.Events", "EventDescription", c => c.String());
            AddColumn("dbo.Events", "EventName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Events", "EventId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.TimeAtPlaces", "Place_Id", "dbo.Places");
            DropForeignKey("dbo.TimeAtPlaces", "Event_Id", "dbo.Events");
            DropPrimaryKey("dbo.TimeAtPlaces");
            DropPrimaryKey("dbo.Places");
            DropPrimaryKey("dbo.Events");
            DropColumn("dbo.TimeAtPlaces", "Votes");
            DropColumn("dbo.TimeAtPlaces", "Time");
            DropColumn("dbo.TimeAtPlaces", "Id");
            DropColumn("dbo.Places", "FourSquareId");
            DropColumn("dbo.Places", "Id");
            DropColumn("dbo.Events", "Link");
            DropColumn("dbo.Events", "Description");
            DropColumn("dbo.Events", "Name");
            DropColumn("dbo.Events", "Id");
            AddPrimaryKey("dbo.TimeAtPlaces", "TimeAtPlaceId");
            AddPrimaryKey("dbo.Places", "PlaceId");
            RenameIndex(table: "dbo.TimeAtPlaces", name: "IX_Place_Id", newName: "IX_Place_PlaceId");
            RenameIndex(table: "dbo.TimeAtPlaces", name: "IX_Event_Id", newName: "IX_Event_EventId");
            RenameColumn(table: "dbo.TimeAtPlaces", name: "Place_Id", newName: "Place_PlaceId");
            RenameColumn(table: "dbo.TimeAtPlaces", name: "Event_Id", newName: "Event_EventId");
            CreateIndex("dbo.Votes", "TimeAtPlace_TimeAtPlaceId");
            CreateIndex("dbo.Votes", "Event_EventId");
            CreateIndex("dbo.Places", "Event_EventId");
            AddForeignKey("dbo.TimeAtPlaces", "Place_PlaceId", "dbo.Places", "PlaceId");
            AddForeignKey("dbo.TimeAtPlaces", "Event_EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Votes", "TimeAtPlace_TimeAtPlaceId", "dbo.TimeAtPlaces", "TimeAtPlaceId");
            AddForeignKey("dbo.Votes", "Event_EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Places", "Event_EventId", "dbo.Events", "EventId");
        }
    }
}
