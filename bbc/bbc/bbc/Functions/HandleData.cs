using bbc.Data.Models;
using bbc.Data.Services.Offline;
using System;
using System.Collections.Generic;
using System.Text;

namespace bbc.Functions
{
    public class HandleData
    {
        public static bool CheckExistLessonInLocalDB(string lessonIdOnline)
        {            
            try
            {
                LessonOfflineService offlineService = new LessonOfflineService();
                List<Lesson> listLesson = offlineService.GetLessonFromLocalDatabase();
                foreach(var lesson in listLesson)
                {
                    if(lessonIdOnline.Trim().Equals(lesson.Id.Trim()))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {

            }
            return true;
        }
    }
}
