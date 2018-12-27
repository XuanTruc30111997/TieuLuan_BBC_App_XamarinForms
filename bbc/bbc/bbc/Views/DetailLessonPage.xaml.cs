using AppConfig;
using bbc.Data.Models;
using bbc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace bbc.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailLessonPage : ContentPage
	{
        public DetailLessonPage(Lesson lesson,string mode)
        {
            InitializeComponent();
            WorkToMode(mode);
            BindingContext = new DetailLessonViewModel(lesson,mode);
            btnPlay.IsVisible = true;
            btnPlay.IsEnabled = true;
            btnPause.IsVisible = false;
            btnPause.IsEnabled = false;
        }
        private void WorkToMode(string mode)
        {
            if(mode.Equals(Mode.Online))
            {
                btnExercise.IsEnabled = true;
                btnExercise.IsVisible = true;

                btnOpenFile.IsEnabled = false;
                btnOpenFile.IsVisible = false;               
            }
            else
            {
                btnExercise.IsVisible = false;
                btnExercise.IsEnabled = false;
                btnOpenFile.IsEnabled = true;
                btnOpenFile.IsVisible = true;
            }
        }

        private void btnPlay_Clicked(object sender, EventArgs e)
        {
            btnPlay.IsVisible = false;
            btnPlay.IsEnabled = false;
            btnPause.IsVisible = true;
            btnPause.IsEnabled = true;
        }

        private void btnPause_Clicked(object sender, EventArgs e)
        {
            btnPlay.IsVisible = true;
            btnPlay.IsEnabled = true;
            btnPause.IsVisible = false;
            btnPause.IsEnabled = false;
        }
    }
}