using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherLocation.CustomControls;
using WeatherLocation.Models;
using WeatherLocation.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace WeatherLocation.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationDetailView : ContentPage
	{
        LocationDetailViewModel ldvm;
        bool isload;
        public LocationDetailView (FavouriteDataTable CurrentAddress,bool isfav=false)
		{
			InitializeComponent ();

            if (!isfav)
            {
                ToolbarItem toolbarItem = new ToolbarItem();
                toolbarItem.Text = "Favourite";
                toolbarItem.SetBinding(ToolbarItem.CommandProperty, "AddFavoriteCommand");
                this.ToolbarItems.Add(toolbarItem);
            }

            ldvm = new LocationDetailViewModel(CurrentAddress);
            BindingContext = ldvm;
            ldvm.BaseNavigation = Navigation;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();     
            if (!isload)
            {
                CustomMap customMap = new CustomMap()
                {
                    MapType = MapType.Street,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                };

                var pin = new CustomPin
                {
                    Type = PinType.Place,
                    Position = new Position(ldvm.CurrentAddress.Latitude, ldvm.CurrentAddress.Longitude),
                    Label = ldvm.CurrentAddress.CountryName,
                    Address = ldvm.CurrentAddress.SubLocality + ", " + ldvm.CurrentAddress.Locality + ", " + ldvm.CurrentAddress.PostalCode + "-" + ldvm.CurrentAddress.CountryCode,
                    Id = "current",
                };

                customMap.CustomPins = new List<CustomPin> { pin };
                customMap.Pins.Add(pin);
                customMap.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromMiles(1.0)));
                mapgrd.Children.Add(customMap);

                await ldvm.GetLocationDetails();
                isload = true;
            }
        }
    }
}