using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Core.Models;

namespace WeatherApp.Core.Services
{
    public interface IWeatherService
    {
        Task<Weather> GetWeatherAsync(string city, CancellationToken token = default(CancellationToken));
    }
}