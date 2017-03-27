using ParserFromExcel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserFromExcel
{
    public class DbWorker
    {
        public async Task AddQuestionThemeRangeToDb(IEnumerable<QuestionTheme> questionTheme)
        {
            var context = new QuestionsContext();
            context.QuestionThemes.AddRange(questionTheme);
            await context.SaveChangesAsync();
        }
        
    }
}
