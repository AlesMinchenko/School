namespace PrSchool.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Subject = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pupils",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidlName = c.String(),
                        Sex = c.String(),
                        Age = c.Int(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        ClassId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.ClassId)
                .Index(t => t.ClassId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidlName = c.String(),
                        Post = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassTeacher",
                c => new
                    {
                        ClassId = c.Int(nullable: false),
                        TeacherId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClassId, t.TeacherId })
                .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.TeacherId, cascadeDelete: true)
                .Index(t => t.ClassId)
                .Index(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClassTeacher", "TeacherId", "dbo.Teachers");
            DropForeignKey("dbo.ClassTeacher", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Pupils", "ClassId", "dbo.Classes");
            DropIndex("dbo.ClassTeacher", new[] { "TeacherId" });
            DropIndex("dbo.ClassTeacher", new[] { "ClassId" });
            DropIndex("dbo.Pupils", new[] { "ClassId" });
            DropTable("dbo.ClassTeacher");
            DropTable("dbo.Subjects");
            DropTable("dbo.Teachers");
            DropTable("dbo.Pupils");
            DropTable("dbo.Classes");
        }
    }
}
