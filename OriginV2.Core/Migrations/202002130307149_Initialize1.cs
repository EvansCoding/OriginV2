namespace OriginV2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Supplier", "Role_Id", "dbo.Role");
            DropForeignKey("dbo.User", "Role_Id", "dbo.Role");
            DropIndex("dbo.Supplier", new[] { "Role_Id" });
            DropIndex("dbo.User", new[] { "Role_Id" });
            DropColumn("dbo.Supplier", "Role_Id");
            DropColumn("dbo.User", "Role_Id");
            DropTable("dbo.Role");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.User", "Role_Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Supplier", "Role_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.User", "Role_Id");
            CreateIndex("dbo.Supplier", "Role_Id");
            AddForeignKey("dbo.User", "Role_Id", "dbo.Role", "Id");
            AddForeignKey("dbo.Supplier", "Role_Id", "dbo.Role", "Id");
        }
    }
}
