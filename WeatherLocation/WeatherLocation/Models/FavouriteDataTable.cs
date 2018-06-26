using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherLocation.Models
{
    public class FavouriteDataTable
    {
        [PrimaryKey, AutoIncrement]
        public int PK { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string FeatureName { get; set; }
        public string PostalCode { get; set; }
        public string SubLocality { get; set; }
        public string Thoroughfare { get; set; }
        public string SubThoroughfare { get; set; }
        public string Locality { get; set; }
        public string AdminArea { get; set; }
        public string SubAdminArea { get; set; }
        [Ignore]
        public string FullAddress { get { return SubLocality + ", " + Locality + ", " + PostalCode + "-" + CountryCode; } }
    }
}
