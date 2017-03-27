namespace ParserFromExcel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        AnswerBody = c.String(nullable: false, maxLength: 200),
                        isRight = c.Boolean(nullable: false),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .Index(t => t.Question_QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionBody = c.String(nullable: false, maxLength: 200),
                        QuestionThemeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.QuestionThemes", t => t.QuestionThemeId, cascadeDelete: true)
                .Index(t => t.QuestionThemeId);
            
            CreateTable(
                "dbo.QuestionThemes",
                c => new
                    {
                        QuestionThemeId = c.Int(nullable: false, identity: true),
                        Theme = c.String(nullable: false, maxLength: 150),
                    })
                .PrimaryKey(t => t.QuestionThemeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "QuestionThemeId", "dbo.QuestionThemes");
            DropForeignKey("dbo.Answers", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.Questions", new[] { "QuestionThemeId" });
            DropIndex("dbo.Answers", new[] { "Question_QuestionId" });
            DropTable("dbo.QuestionThemes");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
