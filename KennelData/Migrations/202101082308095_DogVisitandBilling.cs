namespace KennelData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogVisitandBilling : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogVisitComplete",
                c => new
                    {
                        DogVisitCompleteId = c.Int(nullable: false, identity: true),
                        DogName = c.String(nullable: false),
                        CheckInTime = c.DateTimeOffset(nullable: false, precision: 7),
                        CheckOutTime = c.DateTimeOffset(nullable: false, precision: 7),
                        TotalHoursOnSite = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DogVisitCompleteId);
            
            DropColumn("dbo.DogVisit", "TotalHoursOnSite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DogVisit", "TotalHoursOnSite", c => c.Int(nullable: false));
            DropTable("dbo.DogVisitComplete");
        }
    }
}
