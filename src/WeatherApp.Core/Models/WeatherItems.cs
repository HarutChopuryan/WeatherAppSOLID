using Newtonsoft.Json;

namespace WeatherApp.Core.Models
{
    public class WeatherItems
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }
    }
}