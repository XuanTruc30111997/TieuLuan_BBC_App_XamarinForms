using bbc.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using bbc.Data.Interfaces;

namespace bbc.Data.Services.Offline
{
    public class AnswerOfflineService
    {
        private SQLiteConnection dbConnection;
        private static object collisionLock = new object();

        public List<Answer> Answers { get; set; }

        public AnswerOfflineService()
        {
            dbConnection = DependencyService.Get<ILocalDatabaseConnection>().GetConnection();
            dbConnection.CreateTable<Answer>();

            this.Answers = new List<Answer>(dbConnection.Table<Answer>());
        }

        // GET
        public List<Answer> GetAnswerDb(string idQuestion)
        {
            return dbConnection.Table<Answer>().Where(c => c.QuestionID == idQuestion).ToList();
        }

        // INSERT
        public void AddAnswer(Answer myAnswer)
        {
            dbConnection.Insert(myAnswer);
        }

        // DELETE
        // Xóa tất cả các câu trả lời có trong câu hỏi
        public void DeleteAnswer(string idQuestion)
        {
            dbConnection.Table<Answer>().Delete(c => c.QuestionID == idQuestion);
        }
    }
}
