using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.UI.ViewModels.Main.Implementation
{
    public class Grouping<T> : List<T>
    {
        public string Key { get; set; }
        public Grouping(string key, IEnumerable<T> items) : base(items)
        {
            Key = key;
        }
    }
}
