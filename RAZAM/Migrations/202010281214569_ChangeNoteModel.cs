namespace RAZAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNoteModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notes", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notes", "Status", c => c.String());
        }
    }
}
