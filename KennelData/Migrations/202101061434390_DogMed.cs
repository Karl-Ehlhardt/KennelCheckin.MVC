namespace KennelData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DogMed : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Medication", "Dose", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Medication", "Dose", c => c.Double(nullable: false));
        }
    }
}
