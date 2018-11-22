namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredToPredmet : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Predmets", "naziv", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Predmets", "naziv", c => c.String());
        }
    }
}
