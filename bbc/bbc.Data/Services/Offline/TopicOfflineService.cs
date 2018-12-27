using bbc.Data.Interfaces;
using bbc.Data.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace bbc.Data.Services.Offline
{
    public class TopicOfflineService
    {
        SQLiteConnection sQLiteConnection = null;
        public TopicOfflineService()
        {
            sQLiteConnection=DependencyService.Get<ILocalDatabaseConnection>().GetConnection();
            try
            {
                sQLiteConnection.Execute("CREATE TABLE IF NOT EXISTS Topic(" +
                    "ID TEXT PRIMARY KEY," +
                    " Name TEXT)", 1);
            }
            catch(Exception ex)
            {
                
            }
        }

        public List<Topic> GetTopicFromLocalDatabase()
        {
            try
            {
                var listTopics =sQLiteConnection.Query<Topic>("select * from Topic");
                return listTopics;
            }
            catch(Exception ex)
            {

            }
            return null;
        }
        public void InsertTopicToLocalDatabase(string id,string name)
        {
            try
            {
                sQLiteConnection.Execute("insert into Topic values('" + id + "','" + name + "')");
            }
            catch(Exception ex)
            {

            }
        }
    }
}
