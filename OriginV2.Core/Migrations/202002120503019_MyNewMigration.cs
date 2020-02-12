namespace OriginV2.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MyNewMigration : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Supplier", name: "Supplier_Id", newName: "Account_Id");
            RenameColumn(table: "dbo.User", name: "User_Id", newName: "Account_Id");
            RenameIndex(table: "dbo.Supplier", name: "IX_Supplier_Id", newName: "IX_Account_Id");
            RenameIndex(table: "dbo.User", name: "IX_User_Id", newName: "IX_Account_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.User", name: "IX_Account_Id", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Supplier", name: "IX_Account_Id", newName: "IX_Supplier_Id");
            RenameColumn(table: "dbo.User", name: "Account_Id", newName: "User_Id");
            RenameColumn(table: "dbo.Supplier", name: "Account_Id", newName: "Supplier_Id");
        }
    }
}
