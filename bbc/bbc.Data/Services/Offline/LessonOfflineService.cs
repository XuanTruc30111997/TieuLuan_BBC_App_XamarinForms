using bbc.Data.Interfaces;
using bbc.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace bbc.Data.Services.Offline
{
    public class LessonOfflineService
    {
        SQLiteConnection sQLiteConnection = null;
        public LessonOfflineService()
        {           
            sQLiteConnection = DependencyService.Get<ILocalDatabaseConnection>().GetConnection();
            try
            {
                //sQLiteConnection.Execute("CREATE TABLE IF NOT EXISTS Lesson(" +
                //    "ID TEXT PRIMARY KEY," +
                //    " Name TEXT)"+
                //    "Year INT"+
                //    "IDTopic TEXT"+
                //    "Transcript TEXT" +
                //    "Actor TEXT"+
                //    "Sumary TEXT"+
                //    "Vocabulary TEXT", 1);
                sQLiteConnection.CreateTable<Lesson>();
            }
            catch (Exception ex)
            {

            }
        }

        public List<Lesson> GetLessonFromLocalDatabase()
        {
            try
            {
                var listLessons = sQLiteConnection.Table<Lesson>().ToList();
                return listLessons;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public List<Lesson> GetLessonFromLocalDBToTopic(string topicID)
        {
            try
            {
                var listLessons = sQLiteConnection.Table<Lesson>().Where(lesson => lesson.IdTP == topicID).ToList();
                return listLessons;
            }
            catch(Exception ex)
            {

            }
            return null;
        }
        public void InsertLessonToLocalDatabase(string id, string name,int year,string idTopic,string transcript,
            string actor,string sumary,string vocaburary)
        {
            try
            {
                Lesson lesson = new Lesson();
                lesson.Id = id;
                lesson.Name = name;
                lesson.Year = year;
                lesson.IdTP = idTopic;
                lesson.Transcript = transcript;
                lesson.Actor = actor;
                lesson.Sumary = sumary;
                lesson.Vocabulary = vocaburary;
                lesson.ImageURL = "nophoto.png";
                lesson.FileURLDowload = null;
                lesson.FileURLOnline = null;
                lesson.CreatedDate = null;
                lesson.UpdatedDate = null;
                sQLiteConnection.Insert(lesson);
            }
            catch (Exception ex)
            {

            }
        }
        public void DeleteLesson(string lessonID)
        {
            try
            {
                sQLiteConnection.Query<Lesson>("Delete from Lesson where Id='" + lessonID + "'");
            }
            catch(Exception ex)
            {

            }
        }

    }
}
