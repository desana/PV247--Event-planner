using System.Data.Entity.Migrations;

namespace EventPlanner.Entities.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Link = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TimeAtPlaces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        Votes = c.Int(nullable: false),
                        Event_Id = c.Int(),
                        Place_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.Event_Id)
                .ForeignKey("dbo.Places", t => t.Place_Id)
                .Index(t => t.Event_Id)
                .Index(t => t.Place_Id);
            
            CreateTable(
                "dbo.Places",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FourSquareId = c.String(),
                        FourSquareLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TimeAtPlaces", "Place_Id", "dbo.Places");
            DropForeignKey("dbo.TimeAtPlaces", "Event_Id", "dbo.Events");
            DropIndex("dbo.TimeAtPlaces", new[] { "Place_Id" });
            DropIndex("dbo.TimeAtPlaces", new[] { "Event_Id" });
            DropTable("dbo.Places");
            DropTable("dbo.TimeAtPlaces");
            DropTable("dbo.Events");
        }
    }
}
