using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.DownloadManager;
using System.IO;
using Plugin.DownloadManager.Abstractions;
using System.Linq;

namespace bbc.Droid
{
    [Activity(Label = "bbc", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static MainActivity Instance { get; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AppConfig();
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        private void AppConfig()
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            ImageCircleRenderer.Init();
            Downloaded();
        }
        public void Downloaded()
        {
            CrossDownloadManager.Current.PathNameForDownloadedFile = new System.Func<IDownloadFile, string>(file =>
              {
                  string fileName = Android.Net.Uri.Parse(file.Url).Path.Split('/').Last();
                  return Path.Combine(ApplicationContext.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads).AbsolutePath, fileName);
              });
        }
    }
}