using AppConfig;
using bbc.Data.Models;
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
    public partial class NavigationDrawerPage : MasterDetailPage
    {
        public NavigationDrawerPage(Topic topicItem,string mode)
        {
            InitializeComponent();
            this.Master = new MasterPage(mode);
            this.Detail = new NavigationPage(new LessonPage(topicItem,mode));        
            NavigationPage.SetHasNavigationBar(this, false); //turn off toolbar default
        }

    }
}