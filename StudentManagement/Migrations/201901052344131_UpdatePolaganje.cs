namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePolaganje : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Polaganjes", "ocena", c => c.Int());
            AlterColumn("dbo.Polaganjes", "brojBodova", c => c.Int());
            AlterColumn("dbo.Polaganjes", "brojPokusaja", c => c.Int());
            AlterColumn("dbo.Polaganjes", "polozio", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Polaganjes", "polozio", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Polaganjes", "brojPokusaja", c => c.Int(nullable: false));
            AlterColumn("dbo.Polaganjes", "brojBodova", c => c.Int(nullable: false));
            AlterColumn("dbo.Polaganjes", "ocena", c => c.Int(nullable: false));
        }
    }
}
