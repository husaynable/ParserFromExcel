namespace ParserFromExcel.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class QuestionsContext : DbContext
    {
        public QuestionsContext()
            : base("name=QuestionsContext")
        {
        }
        public DbSet<QuestionTheme> QuestionThemes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
    }
    
}