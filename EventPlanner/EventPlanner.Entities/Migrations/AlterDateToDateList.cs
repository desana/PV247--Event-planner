using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace EventPlanner.Entities.Migrations
{
    class AlterDateToDateList : DbMigration
    {
        public override void Up()
        {
           
            AddColumn("TimeAtPlace", "Url", c => c.String());
        }

        public override void Down()
        {
            DropColumn("TimeAtPlace", "Time");
        }
    }
}

