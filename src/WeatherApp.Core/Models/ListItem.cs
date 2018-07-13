using Newtonsoft.Json;

namespace WeatherApp.Core.Models
{
    public class ListItem
    {
        [JsonProperty("main")]
        public MainItem MainItems { get; set; }
        [JsonProperty("weather")]
        public WeatherItems[] WeatherItems { get; set; }
        [JsonProperty("wind")]
        public WindItem Wind { get; set; }
        [JsonProperty("dt_txt")]
        public string DateTimeText { get; set; }
    }
}
