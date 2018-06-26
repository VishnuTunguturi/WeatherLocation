using Acr.UserDialogs;
using Plugin.Geolocator;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherLocation.Helpers;
using WeatherLocation.Models;
using WeatherLocation.Views;
using Xamarin.Forms;

namespace WeatherLocation.ViewModels
{
    public class LocationViewModel : BaseViewModel
    {
        #region Private Variable
        private ObservableCollection<FavouriteDataTable> _favouriteList;
        private bool _isNoData;
        private FavouriteDataTable CurrentAddress; 
        #endregion

        #region Public Variable
        public ObservableCollection<FavouriteDataTable> FavouriteList
        {
            get { return _favouriteList; }
            set
            {
                SetProperty(ref _favouriteList, value, nameof(FavouriteList));
            }
        }

        public bool IsNoData
        {
            get { return _isNoData; }
            set
            {
                SetProperty(ref _isNoData, value, nameof(IsNoData));
            }
        }

        #endregion

        #region Constructor
        public LocationViewModel()
        {
            Title = "Favourites";
            FavouriteList = new ObservableCollection<FavouriteDataTable>();
            AddCommand = new Command(async () => await AddCommandExecution());
        }
        #endregion

        #region Commands
        public ICommand AddCommand { get; private set; }
       
        private async Task AddCommandExecution()
        {
            await GetCurrentLocation();
            if (CurrentAddress != null)
            {
                await BaseNavigation.PushAsync(new LocationDetailView(CurrentAddress));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
            }
        }
        #endregion

        public async Task GetCurrentLocation()
        {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                    return;

                var locator = CrossGeolocator.Current;
                using (UserDialogs.Instance.Loading("Get Current Location..", null, null, true, MaskType.Black))
                {
                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(5000));
                    if (position == null)
                    {
                        return;
                    }
                    var addresses = await locator.GetAddressesForPositionAsync(position);
                    CurrentAddress = new FavouriteDataTable();
                    var items = addresses.GetEnumerator();
                    while (items.MoveNext())
                    {
                        var current = items.Current;
                        CurrentAddress.AdminArea = current.AdminArea;
                        CurrentAddress.CountryCode = current.CountryCode;
                        CurrentAddress.CountryName = current.CountryName;
                        CurrentAddress.FeatureName = current.FeatureName;
                        CurrentAddress.Latitude = current.Latitude;
                        CurrentAddress.Locality = current.Locality;
                        CurrentAddress.Longitude = current.Longitude;
                        CurrentAddress.PostalCode = current.PostalCode;
                        CurrentAddress.SubAdminArea = current.SubAdminArea;
                        CurrentAddress.SubLocality = current.SubLocality;
                        CurrentAddress.SubThoroughfare = current.SubThoroughfare;
                        CurrentAddress.Thoroughfare = current.Thoroughfare;

                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task GetAllFavouriteLocation()
        {
            try
            {
                FavouriteList = new ObservableCollection<FavouriteDataTable>();
                using (UserDialogs.Instance.Loading("Loading..",null,null,true,MaskType.Black))
                {
                    var result = await App.Database.GetItemsAsync();
                    FavouriteList = new ObservableCollection<FavouriteDataTable>(result);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
            }
            finally
            {
                if (FavouriteList.Count <= 0)
                {
                    IsNoData = true;
                }
                else
                {
                    IsNoData = false;
                }
            }
        }
    }
}
