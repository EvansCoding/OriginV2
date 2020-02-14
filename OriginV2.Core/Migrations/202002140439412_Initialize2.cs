namespace OriginV2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Product", "CreateAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product", "CreateAt", c => c.Boolean(nullable: false));
        }
    }
}
