namespace KennelData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogVisitsadd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogVisit", "CheckInTime", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.DogVisit", "OnSite", c => c.Boolean(nullable: false));
            AddColumn("dbo.DogVisit", "HoursOnSite", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DogVisit", "HoursOnSite");
            DropColumn("dbo.DogVisit", "OnSite");
            DropColumn("dbo.DogVisit", "CheckInTime");
        }
    }
}
