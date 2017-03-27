using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserFromExcel.Models
{
    public class QuestionTheme
    {
        public int QuestionThemeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Theme { get; set; }

        public List<Question> Questions { get; set; }
    }
}
