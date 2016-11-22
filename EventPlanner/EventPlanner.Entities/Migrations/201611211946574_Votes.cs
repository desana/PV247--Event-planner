namespace EventPlanner.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Votes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TimeAtPlaces", "Event_Id", "dbo.Events");
            DropForeignKey("dbo.TimeAtPlaces", "Place_Id", "dbo.Places");
            DropIndex("dbo.TimeAtPlaces", new[] { "Event_Id" });
            DropIndex("dbo.TimeAtPlaces", new[] { "Place_Id" });
            RenameColumn(table: "dbo.TimeAtPlaces", name: "Event_Id", newName: "EventId");
            RenameColumn(table: "dbo.TimeAtPlaces", name: "Place_Id", newName: "PlaceId");
            AlterColumn("dbo.TimeAtPlaces", "EventId", c => c.Int(nullable: false));
            AlterColumn("dbo.TimeAtPlaces", "PlaceId", c => c.Int(nullable: false));
            CreateIndex("dbo.TimeAtPlaces", "PlaceId");
            CreateIndex("dbo.TimeAtPlaces", "EventId");
            AddForeignKey("dbo.TimeAtPlaces", "EventId", "dbo.Events", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TimeAtPlaces", "PlaceId", "dbo.Places", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeAtPlaces", "PlaceId", "dbo.Places");
            DropForeignKey("dbo.TimeAtPlaces", "EventId", "dbo.Events");
            DropIndex("dbo.TimeAtPlaces", new[] { "EventId" });
            DropIndex("dbo.TimeAtPlaces", new[] { "PlaceId" });
            AlterColumn("dbo.TimeAtPlaces", "PlaceId", c => c.Int());
            AlterColumn("dbo.TimeAtPlaces", "EventId", c => c.Int());
            RenameColumn(table: "dbo.TimeAtPlaces", name: "PlaceId", newName: "Place_Id");
            RenameColumn(table: "dbo.TimeAtPlaces", name: "EventId", newName: "Event_Id");
            CreateIndex("dbo.TimeAtPlaces", "Place_Id");
            CreateIndex("dbo.TimeAtPlaces", "Event_Id");
            AddForeignKey("dbo.TimeAtPlaces", "Place_Id", "dbo.Places", "Id");
            AddForeignKey("dbo.TimeAtPlaces", "Event_Id", "dbo.Events", "Id");
        }
    }
}
