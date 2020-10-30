namespace RAZAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserIdTypeChanging : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Files", new[] { "User_Id" });
            DropColumn("dbo.Files", "UserId");
            RenameColumn(table: "dbo.Files", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Files", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Files", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Files", new[] { "UserId" });
            AlterColumn("dbo.Files", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Files", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Files", "UserId", c => c.Int());
            CreateIndex("dbo.Files", "User_Id");
        }
    }
}
