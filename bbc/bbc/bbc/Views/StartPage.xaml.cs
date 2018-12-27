using AppConfig;
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
	public partial class StartPage : ContentPage
	{
		public StartPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false); //turn off toolbar default
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await avatar.ScaleTo(1, 2000);
            await avatar.ScaleTo(0.9, 1500, Easing.Linear);
            await avatar.ScaleTo(150, 1200, Easing.Linear);
            await Navigation.PushAsync(new NavigationDrawerPage(null,Mode.Online));
        }
	}
}