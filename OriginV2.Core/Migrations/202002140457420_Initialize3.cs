namespace OriginV2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Product", new[] { "Parent_Id" });
            AlterColumn("dbo.Product", "Parent_Id", c => c.Guid());
            CreateIndex("dbo.Product", "Parent_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Product", new[] { "Parent_Id" });
            AlterColumn("dbo.Product", "Parent_Id", c => c.Guid(nullable: false));
            CreateIndex("dbo.Product", "Parent_Id");
        }
    }
}
