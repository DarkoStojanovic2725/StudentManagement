namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIdToString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Predmets", new[] { "user_Id" });
            DropColumn("dbo.Predmets", "userId");
            RenameColumn(table: "dbo.Predmets", name: "user_Id", newName: "userId");
            AlterColumn("dbo.Predmets", "userId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Predmets", "userId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Predmets", new[] { "userId" });
            AlterColumn("dbo.Predmets", "userId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Predmets", name: "userId", newName: "user_Id");
            AddColumn("dbo.Predmets", "userId", c => c.Int(nullable: false));
            CreateIndex("dbo.Predmets", "user_Id");
        }
    }
}
