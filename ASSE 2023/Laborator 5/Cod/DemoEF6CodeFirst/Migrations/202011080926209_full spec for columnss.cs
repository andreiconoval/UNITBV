﻿namespace DemoEF6CodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fullspecforcolumnss : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Categories", "Description", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Products", "MeasurementUnit", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Products", "Category_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Customers", "Address", c => c.String(maxLength: 300));
            AlterColumn("dbo.OrderDetails", "Order_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderDetails", "Product_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "Customer_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "Category_Id");
            CreateIndex("dbo.OrderDetails", "Order_Id");
            CreateIndex("dbo.OrderDetails", "Product_Id");
            CreateIndex("dbo.Orders", "Customer_Id");
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "Customer_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropIndex("dbo.Products", new[] { "Category_Id" });
            AlterColumn("dbo.Orders", "Customer_Id", c => c.Int());
            AlterColumn("dbo.OrderDetails", "Product_Id", c => c.Int());
            AlterColumn("dbo.OrderDetails", "Order_Id", c => c.Int());
            AlterColumn("dbo.Customers", "Address", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.Products", "Category_Id", c => c.Int());
            AlterColumn("dbo.Products", "MeasurementUnit", c => c.String());
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Categories", "Description", c => c.String());
            AlterColumn("dbo.Categories", "Name", c => c.String());
            CreateIndex("dbo.Orders", "Customer_Id");
            CreateIndex("dbo.OrderDetails", "Product_Id");
            CreateIndex("dbo.OrderDetails", "Order_Id");
            CreateIndex("dbo.Products", "Category_Id");
            AddForeignKey("dbo.Orders", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products", "Id");
            AddForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id");
        }
    }
}
