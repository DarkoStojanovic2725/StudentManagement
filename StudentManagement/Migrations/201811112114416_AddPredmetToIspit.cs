namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPredmetToIspit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Predmets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        naziv = c.String(),
                        espb = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Ispits", "predmetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Ispits", "predmetId");
            AddForeignKey("dbo.Ispits", "predmetId", "dbo.Predmets", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ispits", "predmetId", "dbo.Predmets");
            DropIndex("dbo.Ispits", new[] { "predmetId" });
            DropColumn("dbo.Ispits", "predmetId");
            DropTable("dbo.Predmets");
        }
    }
}
