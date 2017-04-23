namespace AutoRent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMigrationConfiguration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        brand = c.String(nullable: false),
                        totalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        rentPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isTaken = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CustomerQueries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(),
                        rentStartDate = c.DateTime(nullable: false),
                        rentDays = c.Int(nullable: false),
                        favouriteBrand = c.String(),
                        maxRentPricePerDay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        isCompleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .Index(t => t.CustomerID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        firstName = c.String(nullable: false),
                        lastName = c.String(nullable: false),
                        middleName = c.String(),
                        passportDetails = c.String(nullable: false),
                        phoneNumber = c.String(nullable: false),
                        discountPercentage = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RentDeals",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CarID = c.Int(),
                        CustomerID = c.Int(),
                        CustomerQueryID = c.Int(),
                        dateOfService = c.DateTime(nullable: false),
                        dateOfReturn = c.DateTime(nullable: false),
                        isClosed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cars", t => t.CarID)
                .ForeignKey("dbo.Customers", t => t.CustomerID)
                .ForeignKey("dbo.CustomerQueries", t => t.CustomerQueryID)
                .Index(t => t.CarID)
                .Index(t => t.CustomerID)
                .Index(t => t.CustomerQueryID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        RentID = c.Int(nullable: false),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RentID)
                .ForeignKey("dbo.RentDeals", t => t.RentID)
                .Index(t => t.RentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "RentID", "dbo.RentDeals");
            DropForeignKey("dbo.RentDeals", "CustomerQueryID", "dbo.CustomerQueries");
            DropForeignKey("dbo.RentDeals", "CustomerID", "dbo.Customers");
            DropForeignKey("dbo.RentDeals", "CarID", "dbo.Cars");
            DropForeignKey("dbo.CustomerQueries", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Payments", new[] { "RentID" });
            DropIndex("dbo.RentDeals", new[] { "CustomerQueryID" });
            DropIndex("dbo.RentDeals", new[] { "CustomerID" });
            DropIndex("dbo.RentDeals", new[] { "CarID" });
            DropIndex("dbo.CustomerQueries", new[] { "CustomerID" });
            DropTable("dbo.Payments");
            DropTable("dbo.RentDeals");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerQueries");
            DropTable("dbo.Cars");
        }
    }
}
