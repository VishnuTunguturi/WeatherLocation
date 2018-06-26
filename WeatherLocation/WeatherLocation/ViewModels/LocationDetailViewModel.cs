using Acr.UserDialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherLocation.Helpers;
using WeatherLocation.Models;
using WeatherLocation.Services;
using Xamarin.Forms;

namespace WeatherLocation.ViewModels
{
    public class LocationDetailViewModel : BaseViewModel
    {
        private WeatherLocation.Models.Main _currentTemp;

        #region Public Variable
        public FavouriteDataTable CurrentAddress { get; set; }
        public WeatherLocation.Models.Main CurrentTemp
        {
            get { return _currentTemp; }
            set
            {
                SetProperty(ref _currentTemp, value, nameof(CurrentTemp));
            }
        }
        #endregion

        #region Constructor
        public LocationDetailViewModel(FavouriteDataTable currentAddress)
        {
           
            CurrentAddress = currentAddress;
            if(currentAddress!=null)
                Title = CurrentAddress.CountryName;

            AddFavoriteCommand = new Command(async () => await AddFavoriteCommandExecution());
        }
        #endregion

        #region Commands
        public ICommand AddFavoriteCommand { get; private set; }

        private async Task AddFavoriteCommandExecution()
        {
            await App.Database.SaveItemAsync(CurrentAddress);
            await BaseNavigation.PopAsync();
        }
        #endregion

        public async Task GetLocationDetails()
        {
            try
            {
                
                using (UserDialogs.Instance.Loading("Loading..", null, null, true, MaskType.Black))
                {
                    //var result = await LocalServices.Instance.GetData(String.Format(AppConstans.GetWeatherDetailURL,CurrentAddress.Locality+","+CurrentAddress.CountryCode));
                    var result = await LocalServices.Instance.GetData(String.Format(AppConstans.GetWeatherDetailURL, CurrentAddress.PostalCode + "," + CurrentAddress.CountryCode));
                    if (result.StatusCode == 200)
                    {
                        var data = JsonConvert.DeserializeObject<WeatherResponceModel>(result.Data);
                        CurrentTemp = data.main;
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Uh oh", result.ErrorMsg, "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
            }
        }
    }
}
