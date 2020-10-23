namespace RAZAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DMDeleteUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "RazamUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Files", "RazamUser_Id");
            AddForeignKey("dbo.Files", "RazamUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "RazamUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Files", new[] { "RazamUser_Id" });
            DropColumn("dbo.Files", "RazamUser_Id");
        }
    }
}
