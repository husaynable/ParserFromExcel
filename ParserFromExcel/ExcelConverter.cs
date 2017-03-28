using ClosedXML.Excel;
using ParserFromExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelParsingApp
{
    public class ExcelConverter
    {
        private int numberOfQuestion = 1;
        public event EventHandler<ErrorEventArgs> ErrorEvent;

        public QuestionTheme GetDataModel(string filepath)
        {
            var excel = new XLWorkbook(filepath);
            var sheets = excel.Worksheets;

            numberOfQuestion = 1;

            string themeBody=string.Empty;

            List<Question> questions = new List<Question>();
            foreach (var sheet in sheets)
            {
                var question = getQuestionFromSheet(sheet);

                if (checkQuestion(question))
                {
                    questions.Add(question);

                    if (String.IsNullOrEmpty(themeBody))
                        themeBody = (string)sheet.Cell(1, 3).Value;
                }
                else
                    OnErrorEvent(filepath, numberOfQuestion);

                numberOfQuestion += 1;
            }

            if (!String.IsNullOrEmpty(themeBody))
                return new QuestionTheme { Theme = themeBody, Questions = questions };
            else
                return null;
        }

        private Question getQuestionFromSheet(IXLWorksheet sheet)
        {
            string questionBody = (string)sheet.Cell(2, 3).Value;
            List<Answer> answers = new List<Answer>();

            int row = 5;
            while (!String.IsNullOrEmpty((string)sheet.Cell(row, 2).Value))
            {
                string answer = (string)sheet.Cell(row, 2).Value;

                double isRight = 0;
                sheet.Cell(row, 4).TryGetValue(out isRight);

                if (isRight != 0)
                    answers.Add(new Answer { AnswerBody = answer, isRight = true });
                else
                    answers.Add(new Answer { AnswerBody = answer, isRight = false });

                row += 1;
            }

            return new Question { QuestionBody = questionBody, Answers = answers};
        }

        private bool checkQuestion(Question question)
        {
            if (String.IsNullOrWhiteSpace(question.QuestionBody))
                return false;

            if (question.Answers.Count < 2)
                return false;

            int countOfRightAnswers = 0;
            foreach (var answer in question.Answers)
            {
                if (String.IsNullOrWhiteSpace(answer.AnswerBody))
                    return false;

                if (answer.isRight)
                    countOfRightAnswers += 1;
            }
            if (countOfRightAnswers == 0 || countOfRightAnswers > 1)
                return false;

            return true;
        }

        void OnErrorEvent(string filepath, int num)
        {
            ErrorEvent?.Invoke(this, new ErrorEventArgs { Filepath = filepath, QuestionNumber = num });
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        public string Filepath { get; set; }
        public int QuestionNumber { get; set; }
    }
}
