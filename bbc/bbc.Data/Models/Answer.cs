using System;
using System.Collections.Generic;
using System.Text;

namespace bbc.Data.Models
{
    public class Answer
    {
        public string AnswerID { get; set; }
        public string Content { get; set; }
        public string QuestionID { get; set; }
        public string Correct { get; set; }
    }
}
