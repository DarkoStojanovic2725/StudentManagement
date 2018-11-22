namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPolaganjeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Polaganjes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ocena = c.Int(nullable: false),
                        brojBodova = c.Int(nullable: false),
                        brojPokusaja = c.Int(nullable: false),
                        polozio = c.Boolean(nullable: false),
                        studentId = c.String(maxLength: 128),
                        ispitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Ispits", t => t.ispitId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.studentId)
                .Index(t => t.studentId)
                .Index(t => t.ispitId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Polaganjes", "studentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Polaganjes", "ispitId", "dbo.Ispits");
            DropIndex("dbo.Polaganjes", new[] { "ispitId" });
            DropIndex("dbo.Polaganjes", new[] { "studentId" });
            DropTable("dbo.Polaganjes");
        }
    }
}
