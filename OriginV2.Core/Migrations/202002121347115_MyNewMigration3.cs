namespace OriginV2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyNewMigration3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Supplier", "PathImage", c => c.String());
            AddColumn("dbo.User", "PathImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "PathImage");
            DropColumn("dbo.Supplier", "PathImage");
        }
    }
}
