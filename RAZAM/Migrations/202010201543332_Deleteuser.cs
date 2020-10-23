namespace RAZAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Deleteuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropIndex("dbo.Files", new[] { "UserId" });
            RenameColumn(table: "dbo.Files", name: "RazamUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Files", name: "IX_RazamUser_Id", newName: "IX_User_Id");
            AddForeignKey("dbo.Files", "User_Id", "dbo.AspNetUsers", "Id");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SecondName = c.String(),
                        Nickname = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Files", "User_Id", "dbo.AspNetUsers");
            RenameIndex(table: "dbo.Files", name: "IX_User_Id", newName: "IX_RazamUser_Id");
            RenameColumn(table: "dbo.Files", name: "User_Id", newName: "RazamUser_Id");
            CreateIndex("dbo.Files", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.Users", "Id");
        }
    }
}
