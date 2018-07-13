using Newtonsoft.Json;

namespace WeatherApp.Core.Models
{
    public class WindItem
    {
        [JsonProperty("speed")]
        public double Speed { get; set; }
    }
}