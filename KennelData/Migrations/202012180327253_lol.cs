namespace KennelData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lol : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DogBasic",
                c => new
                    {
                        DogBasicId = c.Int(nullable: false, identity: true),
                        DogName = c.String(nullable: false),
                        Breed = c.String(nullable: false),
                        Weight = c.Double(nullable: false),
                        Size = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DogBasicId);
            
            CreateTable(
                "dbo.DogImage",
                c => new
                    {
                        DogImageId = c.Int(nullable: false, identity: true),
                        ImgFile = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.DogImageId);
            
            CreateTable(
                "dbo.DogInfo",
                c => new
                    {
                        DogInfoId = c.Int(nullable: false, identity: true),
                        DogBasicId = c.Int(nullable: false),
                        OwnerId = c.Int(nullable: false),
                        FoodId = c.Int(nullable: false),
                        SpecialId = c.Int(nullable: false),
                        VetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DogInfoId);
            
            CreateTable(
                "dbo.DogVisit",
                c => new
                    {
                        DogVisitId = c.Int(nullable: false, identity: true),
                        DogInfoId = c.Int(nullable: false),
                        DropOffTime = c.DateTime(nullable: false),
                        PickUpTime = c.DateTime(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.DogVisitId);
            
            CreateTable(
                "dbo.Food",
                c => new
                    {
                        FoodId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AmountPerMeal = c.Double(nullable: false),
                        MorningMeal = c.Boolean(nullable: false),
                        EveningMeal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FoodId);
            
            CreateTable(
                "dbo.Medication",
                c => new
                    {
                        MedicationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Dose = c.Double(nullable: false),
                        MorningMeal = c.Boolean(nullable: false),
                        EveningMeal = c.Boolean(nullable: false),
                        Instructions = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MedicationId);
            
            CreateTable(
                "dbo.Owner",
                c => new
                    {
                        OwnerId = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        BackupName = c.String(nullable: false),
                        BackupPhone = c.String(nullable: false),
                        BackupEmail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.OwnerId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Special",
                c => new
                    {
                        SpecialId = c.Int(nullable: false, identity: true),
                        Instructions = c.String(nullable: false),
                        Allergies = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SpecialId);
            
            CreateTable(
                "dbo.Vet",
                c => new
                    {
                        VetId = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(nullable: false),
                        VetName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.Owner", "ApplicationUserId", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Owner", new[] { "ApplicationUserId" });
            DropTable("dbo.Vet");
            DropTable("dbo.Special");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.Owner");
            DropTable("dbo.Medication");
            DropTable("dbo.Food");
            DropTable("dbo.DogVisit");
            DropTable("dbo.DogInfo");
            DropTable("dbo.DogImage");
            DropTable("dbo.DogBasic");
        }
    }
}
