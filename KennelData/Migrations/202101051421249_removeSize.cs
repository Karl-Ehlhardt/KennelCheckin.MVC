namespace KennelData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeSize : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DogBasic", "Size");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DogBasic", "Size", c => c.String(nullable: false));
        }
    }
}
