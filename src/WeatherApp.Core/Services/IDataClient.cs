using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.Core.Services
{
    public interface IDataClient
    {
        Task<T> GetAsync<T>(string path, Dictionary<string, string> parameters, CancellationToken token = default(CancellationToken));
    }
}