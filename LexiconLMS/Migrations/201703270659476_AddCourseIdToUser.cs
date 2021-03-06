namespace LexiconLMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseIdToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "CourseId", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "CourseId");
            AddForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "CourseId", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "CourseId" });
            DropColumn("dbo.AspNetUsers", "CourseId");
        }
    }
}
