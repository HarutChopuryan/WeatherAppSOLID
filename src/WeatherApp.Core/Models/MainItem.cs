using Newtonsoft.Json;

namespace WeatherApp.Core.Models
{
    public class MainItem
    {
        [JsonProperty("temp")]
        public double Temp { get; set; }
    }
}