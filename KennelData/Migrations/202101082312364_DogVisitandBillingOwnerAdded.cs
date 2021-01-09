namespace KennelData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogVisitandBillingOwnerAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DogVisitComplete", "OwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.DogVisitComplete", "OwnerId");
            AddForeignKey("dbo.DogVisitComplete", "OwnerId", "dbo.Owner", "OwnerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DogVisitComplete", "OwnerId", "dbo.Owner");
            DropIndex("dbo.DogVisitComplete", new[] { "OwnerId" });
            DropColumn("dbo.DogVisitComplete", "OwnerId");
        }
    }
}
