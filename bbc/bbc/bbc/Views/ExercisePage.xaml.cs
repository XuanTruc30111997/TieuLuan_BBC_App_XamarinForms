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
	public partial class ExercisePage : ContentPage
	{
        private ExerciseViewModel examViewModel = new ExerciseViewModel();
        public ExercisePage (Lesson lesson)
		{
            InitializeComponent();
            var myScrollView = new ScrollView { Padding = 10 };
            Content = myScrollView;

            var ahihi = examViewModel.createAuto(lesson);
            myScrollView.Content = ahihi;
            Title = examViewModel.Title;
        }
	}
}