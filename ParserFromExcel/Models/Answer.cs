using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserFromExcel.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }

        [Required]
        [MaxLength(500)]
        public string AnswerBody { get; set; }

        public bool isRight { get; set; }

        public Question Question { get; set; }
    }
}
