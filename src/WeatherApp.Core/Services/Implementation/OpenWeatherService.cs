using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Core.Models;

namespace WeatherApp.Core.Services.Implementation
{
    public class OpenWeatherService : IWeatherService
    {
        private readonly IDataClient _dataClient;

        public OpenWeatherService(IDataClient dataClient)
        {
            _dataClient = dataClient;
        }

        public Task<Weather> GetWeatherAsync(string city, CancellationToken token = default(CancellationToken))
        {
            Dictionary<string, string> parameters = null;
            try
            {
                parameters = new Dictionary<string, string> {["q"] = city};
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return _dataClient.GetAsync<Weather>("/forecast", parameters, token);
        }
    }
}