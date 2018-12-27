using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using bbc.ViewModels;

namespace bbc.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
		public MasterPage (string mode)
		{
			InitializeComponent ();
            BindingContext = new MasterPageViewModel(mode);
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var _masterPageVM = BindingContext as MasterPageViewModel;
            _masterPageVM.TopicItemClick.Execute(null);
        }


    }
}