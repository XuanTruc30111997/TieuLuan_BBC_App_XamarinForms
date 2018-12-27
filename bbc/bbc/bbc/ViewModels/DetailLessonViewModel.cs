using AppConfig;
using bbc.Data.Models;
using bbc.Views;
using Plugin.FilePicker;
using Plugin.MediaManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace bbc.ViewModels
{
    class DetailLessonViewModel : BaseViewModel
    {
        #region properties
        //variable file path (audio file)
        private string _audioPath = null;
        //variable check mode online or offline
        private string _mode = null;
        //variable check mode online or offline
        private int _maximumSlider { get; set; }
        public int MaximumSlider
        {
            get { return _maximumSlider; }
            set
            {
                _maximumSlider = value;
                OnPropertyChanged();
            }
        }
        private Lesson _lesson { get; set; }
        public string NameLesson { get; set; }
        public string Transcript { get; set; }
        private TimeSpan _valueSlider { get; set; }
        public TimeSpan ValueSlider
        {
            get { return _valueSlider; }
            set
            {
                _valueSlider = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region commands
        public ICommand OpenAudioClick
        {
            get
            {
                return new Command(async () =>
                {
                    await OpenAudioFromGallery();
                });
            }
        }
        public ICommand PlayClick
        {
            get
            {
                return new Command(async () =>
                {
                    await DoWhenPlayClickedAsync();
                });
            }
            set { }
        }
        public ICommand PauseClick
        {
            get
            {
                return new Command(async () =>
                {
                    await DoWhenPauseClickedAsync();
                });
            }
            set { }
        }
        public Command ExerciseClick
        {
            get
            {
                return new Command(async () =>
                {
                    await GoToExercisePage();
                });
            }
        }
        #endregion
        public DetailLessonViewModel(Lesson lesson,string mode)
        {
            this._mode = mode;
            this._lesson = lesson;
            ShowLesson();
        }
        private void ShowLesson()
        {
            NameLesson = _lesson.Name;
            Transcript = _lesson.Transcript;
            //_minimumSlider = 0;
            _maximumSlider = 100;
        }
        private async Task OpenAudioFromGallery()
        {
            try
            {
                var file =await CrossFilePicker.Current.PickFile();
                if(file!=null)
                {
                    _audioPath = file.FilePath;
                }
            }
            catch(Exception ex)
            {

            }

        }
        private void ValueSliderChanged()
        {

        }
        private async Task GoToExercisePage()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ExercisePage(_lesson));
        }
        private async Task DoWhenPlayClickedAsync()
        {
            try
            {
                if (_mode.Equals(Mode.Online))
                {
                    await CrossMediaManager.Current.Play(_lesson.FileURLOnline);
                }
                else
                {
                    await CrossMediaManager.Current.Play(_audioPath);

                }
            }
            catch (Exception ex)
            {

            }
            
        }
        private async Task DoWhenPauseClickedAsync()
        {
            try
            {
                await CrossMediaManager.Current.Pause();
            }
            catch (Exception ex)
            {

            }

        }
    }
}
