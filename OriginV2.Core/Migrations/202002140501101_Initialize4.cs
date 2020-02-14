namespace OriginV2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "Parent_Id", "dbo.Product");
            DropIndex("dbo.Product", new[] { "Parent_Id" });
            AddColumn("dbo.Product", "Parent", c => c.String());
            DropColumn("dbo.Product", "Parent_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "Parent_Id", c => c.Guid());
            DropColumn("dbo.Product", "Parent");
            CreateIndex("dbo.Product", "Parent_Id");
            AddForeignKey("dbo.Product", "Parent_Id", "dbo.Product", "Id");
        }
    }
}
