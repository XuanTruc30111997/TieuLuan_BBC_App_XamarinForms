using bbc.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using bbc.Data.Interfaces;

namespace bbc.Data.Services.Offline
{
    public class QuestionOfflineService
    {
        private SQLiteConnection dbConnection;
        private static object collisionLock = new object();

        public List<Question> Questions { get; set; }

        public QuestionOfflineService()
        {
            dbConnection = DependencyService.Get<ILocalDatabaseConnection>().GetConnection();
            dbConnection.CreateTable<Question>();

            this.Questions = new List<Question>(dbConnection.Table<Question>());
        }

        // INSERT
        public void AddQuestion(Question myQuestion)
        {
            dbConnection.Insert(myQuestion);
        }

        // GET
        // Lấy các câu hỏi có trong Lesson
        public List<Question> GetQuestionDb(string idLesson)
        {
            return dbConnection.Table<Question>().Where(c => c.LessonID == idLesson).ToList();

            //var q = from question in dbConnection.Table<Question>()
            //        where question.LessonID == idLesson
            //        select question;
            //return q.ToList();

        }

        // DELETE
        // Sẽ xóa tất cả các câu hỏi của Lesson
        public void DeleteQuestion(string idLesson)
        {
            dbConnection.Table<Question>().Delete(c => c.LessonID == idLesson);
        }
    }
}
