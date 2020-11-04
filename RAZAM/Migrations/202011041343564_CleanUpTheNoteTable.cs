namespace RAZAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CleanUpTheNoteTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Notes", "ReceiverId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "SenderId", "dbo.AspNetUsers");
            DropIndex("dbo.Notes", new[] { "SenderId" });
            DropIndex("dbo.Notes", new[] { "ReceiverId" });
            AlterColumn("dbo.Notes", "SenderId", c => c.String());
            AlterColumn("dbo.Notes", "ReceiverId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "ReceiverId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Notes", "SenderId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Notes", "ReceiverId");
            CreateIndex("dbo.Notes", "SenderId");
            AddForeignKey("dbo.Notes", "SenderId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Notes", "ReceiverId", "dbo.AspNetUsers", "Id");
        }
    }
}
