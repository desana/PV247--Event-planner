namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTimeToList : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.TimeAtPlaces", "Time");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TimeAtPlaces", "Time", c => c.DateTime(nullable: false));
        }
    }
}
