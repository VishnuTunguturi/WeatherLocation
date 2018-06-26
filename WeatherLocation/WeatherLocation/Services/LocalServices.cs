﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WeatherLocation.Models;

namespace WeatherLocation.Services
{
    public class LocalServices
    {
        private HttpClient _baseClient;
        public LocalServices()
        {
            var handler = new HttpClientHandler { UseCookies = false };
            _baseClient = new HttpClient(handler);
            //_baseClient..DefaultRequestHeaders.Add("Content-Type", "application/json");
            _baseClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static LocalServices Instance
        {
            get { return new LocalServices(); }
        }

        //GetData
        public async Task<CommonResponseModel> GetData(string requestUrl)
                responseModel.StatusCode = 501;
                return responseModel;
    }
}