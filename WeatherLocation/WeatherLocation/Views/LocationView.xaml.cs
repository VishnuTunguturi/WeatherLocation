using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLocation.Models;
using WeatherLocation.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherLocation.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationView : ContentPage
	{
        LocationViewModel lvm;
        public LocationView ()
		{
			InitializeComponent ();
            lvm = new LocationViewModel();
            BindingContext = lvm;
            lvm.BaseNavigation = Navigation;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await lvm.GetAllFavouriteLocation();
        }

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var lst = (ListView)sender;
            var data = lst.SelectedItem as FavouriteDataTable;
            if (data != null)
            {
                lst.SelectedItem = null;
                await Navigation.PushAsync(new LocationDetailView(data,true));
            }
        }
    }
}