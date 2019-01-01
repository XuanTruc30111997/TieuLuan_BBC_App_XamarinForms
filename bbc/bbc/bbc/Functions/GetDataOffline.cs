using System;
using System.Collections.Generic;
using System.Text;

using bbc.Data.Services.Offline;
using bbc.Data.Models;

namespace bbc.Functions
{
    public class GetDataOffline
    {
        public static List<Lesson> getLessonOffline()
        {
            LessonOfflineService lessonDB = new LessonOfflineService();
            return lessonDB.GetLessonFromLocalDatabase();
        }

        public static List<Question> getQuetsionOffline(string idLesson)
        {
            QuestionOfflineService questionDB = new QuestionOfflineService();
            return questionDB.GetQuestionDb(idLesson);
        }

        public static List<Answer> getAnswerOffline(string idQuestion)
        {
            AnswerOfflineService answerDB = new AnswerOfflineService();
            return answerDB.GetAnswerDb(idQuestion);
        }

    }
}
