using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using bbc.Data.Models;
using bbc.Data.Services;
using bbc.Interfaces;
using Xamarin.Forms;
using bbc.Views;
using bbc.Data.Services.Offline;
using Plugin.Connectivity;
using AppConfig;
using bbc.Models;

namespace bbc.ViewModels
{
    public class MasterPageViewModel : BaseViewModel
    {
        #region properties
        private RestTopicService topicService = null;
        private TopicOfflineService topicOfflineService = null;
        private bool _aiIsBusy { get; set; }
        public bool AIIsBusy
        {
            get { return _aiIsBusy; }
            set
            {
                _aiIsBusy = value;
                OnPropertyChanged();
            }
        }
        private bool _aiIsVisible { get; set; }
        public bool AIIsVisible
        {
            get { return _aiIsVisible; }
            set
            {
                _aiIsVisible = value;
                OnPropertyChanged();
            }
        }
        //variable mode online or offline
        private string mode = null;
        private string _textMode { get; set; }
        public string TextMode
        {
            get { return _textMode; }
            set
            {
                _textMode = value;
                OnPropertyChanged();
            }
        }
        private List<MasterItem> _listMasterItem { get; set; }
        public List<MasterItem> ListMasterItem
        {
            get
            {
                return _listMasterItem;
            }
            set
            {
                _listMasterItem = value;
                OnPropertyChanged();
            }
        }
        private List<Topic> _listTopic { get; set; }
        public List<Topic> ListTopic
        {
            get { return _listTopic; }
            set
            {
                _listTopic = value;
                OnPropertyChanged();
            }
        }
        private Topic _selectedItem { get; set; }
        public Topic SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Command
        public ICommand TopicItemClick
        {
            get {
                return new Command(async () =>
                {
                    await DoWhenClickTopicItem();
                });
            }
            set { }
        }
        public ICommand ModeClick
        {
            get
            {
                return new Command(async() =>
                {
                    await DoWhenModeClick();
                });
            }
        }
        public ICommand AvatarClick
        {
            get
            {
                return new Command(async () =>
                {
                    await DoWhenClickedAvatar();
                });
            }
        }
        #endregion

        public MasterPageViewModel(string mode)
        {
            //set value for variable mode
            this.mode = mode;
            if(mode.Trim().Equals(Mode.Online))
            {
                _textMode = Mode.Offline;
            }
            else
            {
                _textMode = Mode.Online;
            }
            SetListMasterItem();
            GetTopicAsync().GetAwaiter();
        }
        private async Task GetTopicAsync()
        {
            topicService = new RestTopicService();
            topicOfflineService = new TopicOfflineService();
            //check internet connection
            if (mode.Equals(Mode.Online))
            {
                ListTopic = await topicService.GetDataAsync();
                foreach (var topic in ListTopic)
                {
                    //insert data into local database
                    topicOfflineService.InsertTopicToLocalDatabase(topic.Id, topic.Name);
                }
            }
            else
            {
                ListTopic = topicOfflineService.GetTopicFromLocalDatabase();
            }
            _aiIsBusy = false;
            _aiIsVisible = false;
        }
        private async Task DoWhenModeClick()
        {
            var currentPage = GetCurrentPage();
            if (mode.Equals(Mode.Online))
            {
                await currentPage.Navigation.PushAsync(new NavigationDrawerPage(null, Mode.Offline));
            }
            else
            {
                await currentPage.Navigation.PushAsync(new NavigationDrawerPage(null, Mode.Online));
            }            
            
        }
        private async Task DoWhenClickTopicItem()
        {
            int index = _listTopic.IndexOf(_selectedItem);
            DependencyService.Get<IMessage>().ShortToast(_listTopic[index].Name);
            var currentPage = GetCurrentPage();
            await currentPage.Navigation.PushAsync(new NavigationDrawerPage(_listTopic[index],mode));
        }
        private void SetListMasterItem()
        {
            ListMasterItem = new List<MasterItem>();
            ListMasterItem.Add(new MasterItem("SETTING", "settings.png"));
            ListMasterItem.Add(new MasterItem("INTRO", "intro.png"));
        }
        private async Task DoWhenClickedAvatar()
        {
            try
            {
                var _currentPage = GetCurrentPage();
                await _currentPage.Navigation.PushAsync(new NavigationDrawerPage(null, mode));
            }
            catch(Exception ex)
            {

            }
        }
    }
}
