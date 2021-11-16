namespace LocalBrands.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changekey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Email", "dbo.Customers");
            DropForeignKey("dbo.Payments", "Email", "dbo.Customers");
            DropForeignKey("dbo.Affiliates", "Email", "dbo.Customers");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "CustomerId", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Customers", "Email");
            AddForeignKey("dbo.Orders", "Email", "dbo.Customers", "Email");
            AddForeignKey("dbo.Payments", "Email", "dbo.Customers", "Email", cascadeDelete: true);
            AddForeignKey("dbo.Affiliates", "Email", "dbo.Customers", "Email");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Affiliates", "Email", "dbo.Customers");
            DropForeignKey("dbo.Payments", "Email", "dbo.Customers");
            DropForeignKey("dbo.Orders", "Email", "dbo.Customers");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Affiliates", "Email", "dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Payments", "Email", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Email", "dbo.Customers", "CustomerId");
        }
    }
}
