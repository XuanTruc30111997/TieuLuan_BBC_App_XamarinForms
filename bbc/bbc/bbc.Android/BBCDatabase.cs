using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using bbc.Data.Interfaces;
using SQLite;
using AppConfig;
using Xamarin.Forms;
using bbc.Droid;

[assembly: Dependency(typeof(BBCDatabase))]
namespace bbc.Droid
{
    public class BBCDatabase : ILocalDatabaseConnection
    {
        public SQLiteConnection GetConnection()
        {
            var path = Path.Combine(System.Environment.
            GetFolderPath(System.Environment.
            SpecialFolder.Personal), Constant.localOfflineDB);
            return new SQLiteConnection(path);
        }
    }
}