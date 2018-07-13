using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace WeatherApp.Core.Services.Implementation
{
    public class WebDataClient : IDataClient
    {
        private const string ApiBaseUrl = "https://api.openweathermap.org/data/2.5";
        private const string AppId = "effc073ac9295b1192beaa717c3449c3";

        private readonly HttpClient _client;

        public WebDataClient()
        {
            _client = new HttpClient();
        }

        public async Task<T> GetAsync<T>(string path, Dictionary<string, string> parameters,
            CancellationToken token = default(CancellationToken))
        {
            var uriBuilder = new UriBuilder(ApiBaseUrl);
            uriBuilder.Path += path;
            uriBuilder.Query = GetQueryString(parameters);
            var result = await _client.GetAsync(uriBuilder.Uri, token);
            if (!result.IsSuccessStatusCode)
                throw new Exception($"Http GET request to path {path} failed with status code {result.StatusCode}.");
            var response = await ParseResponse<T>(result);
            return response;
        }

        private string GetQueryString(Dictionary<string, string> parameters)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["APPID"] = AppId;
            foreach (var parameter in parameters) queryString[parameter.Key] = HttpUtility.UrlEncode(parameter.Value);
            return queryString.ToString();
        }

        private async Task<T> ParseResponse<T>(HttpResponseMessage result)
        {
            var jsonResult = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResult);
        }
    }
}