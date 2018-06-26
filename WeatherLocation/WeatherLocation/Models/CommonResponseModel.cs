using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherLocation.Models
{
    public class CommonResponseModel
    {
        public CommonResponseModel()
        {
            ErrorMsg = string.Empty;
            StatusCode = 200;
        }

        public string Data { get; set; }
        public string ErrorMsg { get; set; }
        public int StatusCode { get; set; }
    }
}
