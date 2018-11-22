namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ToEnableUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Polaganjes", "studentId", "dbo.AspNetUsers");
            DropIndex("dbo.Polaganjes", new[] { "studentId" });
            AlterColumn("dbo.Polaganjes", "studentId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Polaganjes", "studentId");
            AddForeignKey("dbo.Polaganjes", "studentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Polaganjes", "studentId", "dbo.AspNetUsers");
            DropIndex("dbo.Polaganjes", new[] { "studentId" });
            AlterColumn("dbo.Polaganjes", "studentId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Polaganjes", "studentId");
            AddForeignKey("dbo.Polaganjes", "studentId", "dbo.AspNetUsers", "Id");
        }
    }
}
