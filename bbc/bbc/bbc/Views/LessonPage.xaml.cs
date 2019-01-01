using bbc.Data.Models;
using bbc.Data.Services;
using bbc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Connectivity;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppConfig;
using bbc.Functions;
using bbc.Interfaces;

namespace bbc.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LessonPage : ContentPage
    {
        private string _mode = null;
        public LessonPage(Topic topicItem, string mode)
        {
            InitializeComponent();
            BindingContext = new LessonViewModel(topicItem, mode);
            if (topicItem != null)
            {
                this.Title = topicItem.Name;
            }
            else
            {
                this.Title = "ALL LESSONS";
            }
            _mode = mode;
            activity.IsVisible = true;
            activity.IsRunning = true;
            activity.IsEnabled = true;

            var button = lvLesson.FindByName<Button>("btnDownload");
            if (button != null)
            {
                button.Image = "download.png";
            }

        }

        protected async override void OnAppearing()
        {
            RestLessonService restLessonService = new RestLessonService();
            await restLessonService.GetDataAsync();
            activity.IsVisible = false;
            activity.IsRunning = false;
            activity.IsEnabled = false;
            base.OnAppearing();
        }
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var _lessonPageVM = BindingContext as LessonViewModel;
            _lessonPageVM.ItemListViewClick.Execute(null);
        }

        private async void ImageDownloadClick(object sender, TappedEventArgs args)
        {
            Button btnDownload = (Button)sender;
            //get Id Item ListView Lesson by CommanParameter
            // string Id = btnDownload.CommandParameter.ToString();

            var _lessonPageVM = BindingContext as LessonViewModel;
            _lessonPageVM.IdItemListLesson = btnDownload.CommandParameter.ToString();

            if (_mode.Equals(Mode.Online))
            {
                // _lessonPageVM.ImgDownloadAudio.Execute(Id);

                // bool isDone = await _lessonPageVM.DownloadOrDelete();
                if (await _lessonPageVM.DownloadOrDelete())
                {
                    btnDownload.Image = "downloaded.png";
                    btnDownload.IsEnabled = false;
                    DependencyService.Get<IMessage>().ShortToast("Downloading");
                }
                else
                    DependencyService.Get<IMessage>().ShortToast("Cancel Download");
            }
            else
            {
                // bool isDone = await _lessonPageVM.DownloadOrDelete();
                if (await _lessonPageVM.DownloadOrDelete())
                    DependencyService.Get<IMessage>().ShortToast("Delete Completed");
                else
                    DependencyService.Get<IMessage>().ShortToast("Cancel Delete");
            }
        }

        private void Handler_SearchChanged(object sender, TextChangedEventArgs e)
        {
            var _lessonPageVM = BindingContext as LessonViewModel;
            _lessonPageVM.SearchLesson.Execute(null);
        }
    }
}