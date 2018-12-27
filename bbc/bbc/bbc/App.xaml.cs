using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using bbc.Views;
using bbc.Data.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace bbc
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new StartPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
