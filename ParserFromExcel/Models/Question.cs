using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserFromExcel.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        [Required]
        [MaxLength(500)]
        public string QuestionBody { get; set; }

        public int QuestionThemeId { get; set; }
        public QuestionTheme Theme { get; set; }

        public List<Answer> Answers { get; set; }
    }
}
