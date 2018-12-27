using System;
using System.Collections.Generic;
using System.Text;

namespace bbc.Data.Models
{
    public class Question
    {
        public string QuestionID { get; set; }
        public string Content { get; set; }
        public int TypeQuestion { get; set; }
        public string LessonID { get; set; }
    }
}
