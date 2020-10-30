namespace RAZAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNoteTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(),
                        Receiver_Id = c.String(maxLength: 128),
                        Sender_Id = c.String(maxLength: 128),
                        RazamUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.Receiver_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Sender_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.RazamUser_Id)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id)
                .Index(t => t.RazamUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "RazamUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "Sender_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "Receiver_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Notes", new[] { "RazamUser_Id" });
            DropIndex("dbo.Notes", new[] { "Sender_Id" });
            DropIndex("dbo.Notes", new[] { "Receiver_Id" });
            DropTable("dbo.Notes");
        }
    }
}
