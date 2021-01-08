namespace KennelData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogVisitTotalHoursOnSite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogVisit", "TotalHoursOnSite", c => c.Int(nullable: false));
            DropColumn("dbo.DogVisit", "HoursOnSite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DogVisit", "HoursOnSite", c => c.Int(nullable: false));
            DropColumn("dbo.DogVisit", "TotalHoursOnSite");
        }
    }
}
