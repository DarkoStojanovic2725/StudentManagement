namespace StudentManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatePredmeti : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Predmets (naziv, espb) VALUES ('CS324-Skripting jezici', 8)");
        }
        
        public override void Down()
        {
        }
    }
}
