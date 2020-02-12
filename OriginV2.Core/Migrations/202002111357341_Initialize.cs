namespace OriginV2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 500),
                        Address = c.String(nullable: false, maxLength: 500),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(),
                        Role_Id = c.Guid(nullable: false),
                        Supplier_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role_Id)
                .ForeignKey("dbo.Account", t => t.Supplier_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        ImageOne = c.String(nullable: false),
                        ImageTwo = c.String(nullable: false),
                        Origin = c.String(nullable: false),
                        Attribute = c.String(nullable: false),
                        QRHashCode = c.String(),
                        Technology = c.String(nullable: false),
                        Publish = c.Boolean(nullable: false),
                        HarvestAt = c.DateTime(nullable: false),
                        CreateAt = c.Boolean(nullable: false),
                        Parent_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                        Supplier_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Parent_Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.Supplier", t => t.Supplier_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Supplier_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FullName = c.String(nullable: false, maxLength: 500),
                        Email = c.String(),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(),
                        Role_Id = c.Guid(nullable: false),
                        User_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.Role_Id)
                .ForeignKey("dbo.Account", t => t.User_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductView",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        ImageOne = c.String(nullable: false),
                        ImageTwo = c.String(nullable: false),
                        Origin = c.String(nullable: false),
                        Attribute = c.String(nullable: false),
                        Technology = c.String(nullable: false),
                        QRHashCode = c.String(),
                        HarvestAt = c.DateTime(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                        UpdateAt = c.DateTime(),
                        Supplier_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Supplier", t => t.Supplier_Id)
                .Index(t => t.Supplier_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User", "User_Id", "dbo.Account");
            DropForeignKey("dbo.Supplier", "Supplier_Id", "dbo.Account");
            DropForeignKey("dbo.ProductView", "Supplier_Id", "dbo.Supplier");
            DropForeignKey("dbo.Product", "Supplier_Id", "dbo.Supplier");
            DropForeignKey("dbo.User", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.Supplier", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.Product", "User_Id", "dbo.User");
            DropForeignKey("dbo.Product", "Parent_Id", "dbo.Product");
            DropIndex("dbo.ProductView", new[] { "Supplier_Id" });
            DropIndex("dbo.User", new[] { "User_Id" });
            DropIndex("dbo.User", new[] { "Role_Id" });
            DropIndex("dbo.Product", new[] { "Supplier_Id" });
            DropIndex("dbo.Product", new[] { "User_Id" });
            DropIndex("dbo.Product", new[] { "Parent_Id" });
            DropIndex("dbo.Supplier", new[] { "Supplier_Id" });
            DropIndex("dbo.Supplier", new[] { "Role_Id" });
            DropTable("dbo.ProductView");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Product");
            DropTable("dbo.Supplier");
            DropTable("dbo.Account");
        }
    }
}
