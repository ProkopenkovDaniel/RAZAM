namespace RAZAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGuidForFileModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "GuidName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "GuidName");
        }
    }
}
