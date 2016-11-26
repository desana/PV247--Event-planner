namespace EventPlanner.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateToNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeAtPlaces", "Time", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeAtPlaces", "Time", c => c.DateTime(nullable: false));
        }
    }
}
