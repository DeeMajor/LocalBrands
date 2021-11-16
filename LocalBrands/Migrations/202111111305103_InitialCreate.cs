namespace LocalBrands.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.AdminID);
            
            CreateTable(
                "dbo.Affiliate_Joiner",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Affiliate_Key = c.Guid(nullable: false),
                        Email = c.String(),
                        New_Customer_Email = c.String(),
                        used = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Affiliates", t => t.Affiliate_Key, cascadeDelete: true)
                .Index(t => t.Affiliate_Key);
            
            CreateTable(
                "dbo.Affiliates",
                c => new
                    {
                        Affiliate_Key = c.Guid(nullable: false, identity: true),
                        Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Affiliate_Key)
                .ForeignKey("dbo.Customers", t => t.Email)
                .Index(t => t.Email);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        phone = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Order_ID = c.String(nullable: false, maxLength: 128),
                        date_created = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 128),
                        shipped = c.Boolean(nullable: false),
                        status = c.String(),
                        packed = c.Boolean(nullable: false),
                        ConfirmationCode = c.String(),
                        Driver_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Order_ID)
                .ForeignKey("dbo.Customers", t => t.Email)
                .ForeignKey("dbo.Drivers", t => t.Driver_ID)
                .Index(t => t.Email)
                .Index(t => t.Driver_ID);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverID = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Busy = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DriverID);
            
            CreateTable(
                "dbo.Order_Item",
                c => new
                    {
                        Order_Item_id = c.Int(nullable: false, identity: true),
                        Order_id = c.String(maxLength: 128),
                        item_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                        accepted = c.Boolean(nullable: false),
                        date_accepted = c.DateTime(),
                        shipped = c.Boolean(nullable: false),
                        date_shipped = c.DateTime(),
                        Return = c.Boolean(nullable: false),
                        ReturnReason = c.String(),
                    })
                .PrimaryKey(t => t.Order_Item_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_id)
                .Index(t => t.Order_id)
                .Index(t => t.item_id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemCode = c.Int(nullable: false, identity: true),
                        Category_ID = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 80),
                        Description = c.String(nullable: false, maxLength: 255),
                        QuantityInStock = c.Int(nullable: false),
                        Picture = c.Binary(),
                        Price = c.Double(nullable: false),
                        ReviewId = c.Int(nullable: false),
                        CreateBy = c.String(),
                    })
                .PrimaryKey(t => t.ItemCode)
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.Cart_Item",
                c => new
                    {
                        cart_item_id = c.String(nullable: false, maxLength: 128),
                        cart_id = c.String(maxLength: 128),
                        item_id = c.Int(nullable: false),
                        quantity = c.Int(nullable: false),
                        price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.cart_item_id)
                .ForeignKey("dbo.Carts", t => t.cart_id)
                .ForeignKey("dbo.Items", t => t.item_id, cascadeDelete: true)
                .Index(t => t.cart_id)
                .Index(t => t.item_id);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        cart_id = c.String(nullable: false, maxLength: 128),
                        date_created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.cart_id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Category_ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 80),
                        Department_ID = c.Int(nullable: false),
                        CreateBy = c.String(),
                    })
                .PrimaryKey(t => t.Category_ID)
                .ForeignKey("dbo.Departments", t => t.Department_ID, cascadeDelete: true)
                .Index(t => t.Department_ID);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Department_ID = c.Int(nullable: false, identity: true),
                        Department_Name = c.String(nullable: false, maxLength: 80),
                        Description = c.String(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Department_ID);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        payment_ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 128),
                        Date = c.DateTime(nullable: false),
                        AmountPaid = c.Double(nullable: false),
                        PaymentFor = c.String(nullable: false),
                        PaymentMethod = c.String(nullable: false),
                        Order_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.payment_ID)
                .ForeignKey("dbo.Customers", t => t.Email, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.Email)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.Shipping_Address",
                c => new
                    {
                        Address_ID = c.Int(nullable: false, identity: true),
                        Building_Name = c.String(),
                        Floor = c.String(),
                        Contact_Number = c.String(),
                        Address_Type = c.String(),
                        Comments = c.String(),
                        Order_ID = c.String(maxLength: 128),
                        street_number = c.Int(nullable: false),
                        street_name = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Address_ID)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.Order_ID);
            
            CreateTable(
                "dbo.BrandOwners",
                c => new
                    {
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        BrandOwnerName = c.String(),
                        BrandOwnerSurname = c.String(),
                        BrandOwnerEmail = c.String(),
                        ContactNumber = c.String(),
                        Address = c.String(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Ref = c.Int(nullable: false, identity: true),
                        Affiliate_Key = c.String(),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remaining_Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Transaction_Date = c.DateTime(nullable: false),
                        Joiner_Email = c.String(),
                        To_Affiliate = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Ref);
            
            CreateTable(
                "dbo.Order_Tracking",
                c => new
                    {
                        tracking_ID = c.Int(nullable: false, identity: true),
                        order_ID = c.String(maxLength: 128),
                        date = c.DateTime(nullable: false),
                        status = c.String(),
                        Recipient = c.String(),
                    })
                .PrimaryKey(t => t.tracking_ID)
                .ForeignKey("dbo.Orders", t => t.order_ID)
                .Index(t => t.order_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.SubscriptionPrices",
                c => new
                    {
                        SubscriptionPriceId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.SubscriptionPriceId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WithdrawLevels",
                c => new
                    {
                        Level_ID = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Level_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Order_Tracking", "order_ID", "dbo.Orders");
            DropForeignKey("dbo.Affiliate_Joiner", "Affiliate_Key", "dbo.Affiliates");
            DropForeignKey("dbo.Affiliates", "Email", "dbo.Customers");
            DropForeignKey("dbo.Shipping_Address", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.Payments", "Order_ID", "dbo.Orders");
            DropForeignKey("dbo.Payments", "Email", "dbo.Customers");
            DropForeignKey("dbo.Order_Item", "Order_id", "dbo.Orders");
            DropForeignKey("dbo.Order_Item", "item_id", "dbo.Items");
            DropForeignKey("dbo.Items", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Department_ID", "dbo.Departments");
            DropForeignKey("dbo.Cart_Item", "item_id", "dbo.Items");
            DropForeignKey("dbo.Cart_Item", "cart_id", "dbo.Carts");
            DropForeignKey("dbo.Orders", "Driver_ID", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "Email", "dbo.Customers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Order_Tracking", new[] { "order_ID" });
            DropIndex("dbo.Shipping_Address", new[] { "Order_ID" });
            DropIndex("dbo.Payments", new[] { "Order_ID" });
            DropIndex("dbo.Payments", new[] { "Email" });
            DropIndex("dbo.Categories", new[] { "Department_ID" });
            DropIndex("dbo.Cart_Item", new[] { "item_id" });
            DropIndex("dbo.Cart_Item", new[] { "cart_id" });
            DropIndex("dbo.Items", new[] { "Category_ID" });
            DropIndex("dbo.Order_Item", new[] { "item_id" });
            DropIndex("dbo.Order_Item", new[] { "Order_id" });
            DropIndex("dbo.Orders", new[] { "Driver_ID" });
            DropIndex("dbo.Orders", new[] { "Email" });
            DropIndex("dbo.Affiliates", new[] { "Email" });
            DropIndex("dbo.Affiliate_Joiner", new[] { "Affiliate_Key" });
            DropTable("dbo.WithdrawLevels");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SubscriptionPrices");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Order_Tracking");
            DropTable("dbo.Transactions");
            DropTable("dbo.BrandOwners");
            DropTable("dbo.Shipping_Address");
            DropTable("dbo.Payments");
            DropTable("dbo.Departments");
            DropTable("dbo.Categories");
            DropTable("dbo.Carts");
            DropTable("dbo.Cart_Item");
            DropTable("dbo.Items");
            DropTable("dbo.Order_Item");
            DropTable("dbo.Drivers");
            DropTable("dbo.Orders");
            DropTable("dbo.Customers");
            DropTable("dbo.Affiliates");
            DropTable("dbo.Affiliate_Joiner");
            DropTable("dbo.Admins");
        }
    }
}
