using AppConfig;
using bbc.Data.Models;
using bbc.Data.Services;
using bbc.Data.Services.Offline;
using bbc.Functions;
using bbc.Interfaces;
using bbc.Views;
using Plugin.Connectivity;
using Plugin.DownloadManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace bbc.ViewModels
{
    class LessonViewModel : BaseViewModel
    {
        #region properties
        private RestLessonService restLessonService = null;
        //private string _imageDownload { get; set; }
        //public string ImageDownload
        //{
        //    get { return _imageDownload; }
        //    set
        //    {
        //        _imageDownload = value;
        //        OnPropertyChanged();
        //    }
        //}
        //variable check mode online or offline
        private string mode = null;
        private string _idItemListLesson { get; set; }
        public string IdItemListLesson
        {
            get
            {
                return _idItemListLesson;
            }
            set
            {
                _idItemListLesson = value;
                OnPropertyChanged();
            }
        }
        private Lesson _selectedItem { get; set; }
        public Lesson SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        private List<Lesson> _listLesson { get; set; }
        public List<Lesson> ListLesson
        {
            get
            {
                return _listLesson;
            }
            set
            {
                _listLesson = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region commands
        public ICommand ItemListViewClick
        {
            get
            {
                return new Command(async () =>
                {
                    await GoToAudioPage();
                });
            }
            set { }
        }
        //click image download on list lesson
        public ICommand ImgDownloadAudio
        {
            get
            {
                return new Command(async () =>
                {
                    await DownloadAudio();
                });
            }
            set { }
        }
        #endregion

        public LessonViewModel(Topic topicItem,string mode)
        {
            this.mode = mode;
            ShowListLesson(topicItem).GetAwaiter();
        }
        private async Task ShowListLesson(Topic topicItem)
        {
            restLessonService = new RestLessonService();
            LessonOfflineService lessonOfflineService = new LessonOfflineService();
            //check internet connection
            if(mode.Equals(Mode.Online))
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (topicItem != null)
                    {
                        ListLesson = await restLessonService.GetDataByTopic(topicItem.Id);
                        //set value for image download
                        foreach (var lesson in ListLesson)
                        {
                            if(HandleData.CheckExistLessonInLocalDB(lesson.Id)==true)
                            {
                                lesson.ImageDownload = "downloaded.png";
                            }
                            else
                            {
                                lesson.ImageDownload = "download.png";
                            }
                            
                        }
                    }
                    else
                    {
                        ListLesson = await restLessonService.GetDataAsync();
                        //set value for image download
                        foreach (var lesson in ListLesson)
                        {
                            if (HandleData.CheckExistLessonInLocalDB(lesson.Id) == true)
                            {
                                lesson.ImageDownload = "downloaded.png";
                            }
                            else
                            {
                                lesson.ImageDownload = "download.png";
                            }
                        }
                        
                    }
                }
                else
                {                  
                    var _currentPage = GetCurrentPage();
                    var action=await _currentPage.DisplayAlert("Cannot connect internet!Please check internect connection or use Offline Mode!", "Would you like to use Offline Mode?", "OK","Cancel");
                    if(action == true)
                    {
                        await _currentPage.Navigation.PushAsync(new NavigationDrawerPage(null, Mode.Offline));
                    }
                    //DependencyService.Get<IMessage>().ShortToast("Cannot connect internet!Please check internect connection or use Offline Mode");
                }
            }
            else
            {
                //ImageDownload = "delete.png";
                if (topicItem!=null)
                {
                    ListLesson = lessonOfflineService.GetLessonFromLocalDBToTopic(topicItem.Id);
                    //set value for image download
                    foreach (var lesson in ListLesson)
                    {
                        lesson.ImageDownload = "delete.png";
                    }
                }
                else
                {
                    ListLesson = lessonOfflineService.GetLessonFromLocalDatabase();
                    //set value for image download
                    foreach (var lesson in ListLesson)
                    {
                        lesson.ImageDownload = "delete.png";
                    }
                }
                
            }
           

        }
        private async Task GoToAudioPage()
        {
            int _positionItem = _listLesson.IndexOf(_selectedItem);
            var _currentPage = GetCurrentPage();
            //await _currentPage.TranslateTo(-_currentPage.Width, 0, 500, Easing.SpringOut);
            await _currentPage.Navigation.PushAsync(new DetailLessonPage(_listLesson[_positionItem],mode));
        }
        private async Task DownloadAudio()
        {
            try
            {
                LessonOfflineService offlineService = new LessonOfflineService();
                if (mode.Equals(Mode.Online))
                {
                    //get URL of Audio follow lessonID
                    string urlAudio = null;
                    foreach (var lesson in _listLesson)
                    {
                        if (lesson.Id.Trim().Equals(_idItemListLesson.Trim()))
                        {
                            urlAudio = lesson.FileURLOnline.ToString();
                            break;
                        }
                    }
                    await Task.Run(() =>
                    {
                        var downloadManager = CrossDownloadManager.Current;
                        var file = downloadManager.CreateDownloadFile(urlAudio);
                        downloadManager.Start(file, true);
                    });

                    restLessonService = new RestLessonService();
                    //get lesson item follow id
                    Lesson lessonItem = await restLessonService.GetLessonByID(_idItemListLesson.ToString());
                    //Save lesson item into local database when click dowload image
                    offlineService.InsertLessonToLocalDatabase(lessonItem.Id, lessonItem.Name, lessonItem.Year,
                        lessonItem.IdTP, lessonItem.Transcript, lessonItem.Actor, lessonItem.Sumary, lessonItem.Vocabulary);
                    DependencyService.Get<IMessage>().ShortToast("index=" + _idItemListLesson);
                }
                else
                {   
                    //delete lesson
                    var _currentPage = GetCurrentPage();
                    var action = await _currentPage.DisplayAlert("Question!", "Are you sure you want to delete lesson item offline?", "OK", "Cancel");
                    if (action == true)
                    {
                        offlineService.DeleteLesson(_idItemListLesson);
                        await _currentPage.Navigation.PushAsync(new NavigationDrawerPage(null, Mode.Offline));
                    }
                }
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
