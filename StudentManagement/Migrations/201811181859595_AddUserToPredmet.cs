namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToPredmet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Predmets", "userId", c => c.Int(nullable: false));
            AddColumn("dbo.Predmets", "user_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Predmets", "user_Id");
            AddForeignKey("dbo.Predmets", "user_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Predmets", "user_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Predmets", new[] { "user_Id" });
            DropColumn("dbo.Predmets", "user_Id");
            DropColumn("dbo.Predmets", "userId");
        }
    }
}
