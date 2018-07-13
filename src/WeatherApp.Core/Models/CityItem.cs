using Newtonsoft.Json;

namespace WeatherApp.Core.Models
{
    public class CityItem
    {
        [JsonProperty("name")]
        public string CityName { get; set; }
        [JsonProperty("coord")]
        public Point Coordinate { get; set; }
    }
}