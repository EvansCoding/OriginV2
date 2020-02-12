namespace OriginV2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyNewMigration2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Supplier", new[] { "Account_Id" });
            DropIndex("dbo.User", new[] { "Account_Id" });
            AlterColumn("dbo.Supplier", "Account_Id", c => c.Guid());
            AlterColumn("dbo.User", "Account_Id", c => c.Guid());
            CreateIndex("dbo.Supplier", "Account_Id");
            CreateIndex("dbo.User", "Account_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Account_Id" });
            DropIndex("dbo.Supplier", new[] { "Account_Id" });
            AlterColumn("dbo.User", "Account_Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Supplier", "Account_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.User", "Account_Id");
            CreateIndex("dbo.Supplier", "Account_Id");
        }
    }
}
