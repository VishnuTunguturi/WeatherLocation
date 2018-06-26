using System;
using System.IO;
using WeatherLocation.Data;
using WeatherLocation.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace WeatherLocation
{
	public partial class App : Application
	{
        static FavouriteDataBase database;
        public App ()
		{
			InitializeComponent();

			MainPage =new NavigationPage(new LocationView());
		}

        public static FavouriteDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database = new FavouriteDataBase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "locationSQLite.db3"));
                }
                return database;
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
