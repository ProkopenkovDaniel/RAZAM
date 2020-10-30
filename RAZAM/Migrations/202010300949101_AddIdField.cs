namespace RAZAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIdField : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "RazamUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Notes", new[] { "RazamUser_Id" });
            AddColumn("dbo.Notes", "RazamUser_Id1", c => c.String(maxLength: 128));
            AlterColumn("dbo.Notes", "RazamUser_Id", c => c.String());
            CreateIndex("dbo.Notes", "RazamUser_Id1");
            AddForeignKey("dbo.Notes", "RazamUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "RazamUser_Id1", "dbo.AspNetUsers");
            DropIndex("dbo.Notes", new[] { "RazamUser_Id1" });
            AlterColumn("dbo.Notes", "RazamUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Notes", "RazamUser_Id1");
            CreateIndex("dbo.Notes", "RazamUser_Id");
            AddForeignKey("dbo.Notes", "RazamUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
