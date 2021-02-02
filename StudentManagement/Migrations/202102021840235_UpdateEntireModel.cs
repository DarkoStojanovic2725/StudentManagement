namespace StudentManagement.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEntireModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Predmets", "userId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ispits", "predmetId", "dbo.Predmets");
            DropForeignKey("dbo.Polaganjes", "ispitId", "dbo.Ispits");
            DropForeignKey("dbo.Polaganjes", "studentId", "dbo.AspNetUsers");
            DropIndex("dbo.Ispits", new[] { "predmetId" });
            DropIndex("dbo.Predmets", new[] { "userId" });
            DropIndex("dbo.Polaganjes", new[] { "studentId" });
            DropIndex("dbo.Polaganjes", new[] { "ispitId" });
            CreateTable(
                "dbo.ExamResults",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Grade = c.Int(),
                        Score = c.Int(),
                        NumberOfAttempts = c.Int(),
                        Passed = c.Boolean(),
                        StudentId = c.String(nullable: false, maxLength: 128),
                        ExamId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Exams", t => t.ExamId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.ExamId);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfExam = c.DateTime(nullable: false),
                        Subjectid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subjectid, cascadeDelete: true)
                .Index(t => t.Subjectid);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Espb = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            DropTable("dbo.Ispits");
            DropTable("dbo.Predmets");
            DropTable("dbo.Polaganjes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Polaganjes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ocena = c.Int(),
                        brojBodova = c.Int(),
                        brojPokusaja = c.Int(),
                        polozio = c.Boolean(),
                        studentId = c.String(nullable: false, maxLength: 128),
                        ispitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Predmets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        naziv = c.String(nullable: false),
                        espb = c.Int(nullable: false),
                        userId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Ispits",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        datumIspita = c.DateTime(nullable: false),
                        predmetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            DropForeignKey("dbo.ExamResults", "StudentId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ExamResults", "ExamId", "dbo.Exams");
            DropForeignKey("dbo.Exams", "Subjectid", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Subjects", new[] { "UserId" });
            DropIndex("dbo.Exams", new[] { "Subjectid" });
            DropIndex("dbo.ExamResults", new[] { "ExamId" });
            DropIndex("dbo.ExamResults", new[] { "StudentId" });
            DropTable("dbo.Subjects");
            DropTable("dbo.Exams");
            DropTable("dbo.ExamResults");
            CreateIndex("dbo.Polaganjes", "ispitId");
            CreateIndex("dbo.Polaganjes", "studentId");
            CreateIndex("dbo.Predmets", "userId");
            CreateIndex("dbo.Ispits", "predmetId");
            AddForeignKey("dbo.Polaganjes", "studentId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Polaganjes", "ispitId", "dbo.Ispits", "id", cascadeDelete: true);
            AddForeignKey("dbo.Ispits", "predmetId", "dbo.Predmets", "id", cascadeDelete: true);
            AddForeignKey("dbo.Predmets", "userId", "dbo.AspNetUsers", "Id");
        }
    }
}
