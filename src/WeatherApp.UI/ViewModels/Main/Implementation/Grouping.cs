using System.Collections.Generic;

namespace WeatherApp.UI.ViewModels.Main.Implementation
{
    public class Grouping<T> : List<T>
    {
        public Grouping(string key, IEnumerable<T> Items) : base(Items)
        {
            Key = key;
            ItemsViewModel = Items;
        }

        public string Key { get; set; }
        public IEnumerable<T> ItemsViewModel { get; set; }
    }
}