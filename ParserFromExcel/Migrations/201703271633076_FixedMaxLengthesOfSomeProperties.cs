namespace ParserFromExcel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedMaxLengthesOfSomeProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "AnswerBody", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Questions", "QuestionBody", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "QuestionBody", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Answers", "AnswerBody", c => c.String(nullable: false, maxLength: 200));
        }
    }
}
