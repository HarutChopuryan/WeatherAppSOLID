using System;
using System.Collections.Generic;
using System.Text;
using PropertyChanged;
using WeatherApp.Core.Models;
using WeatherApp.UI.ViewModels.Base.Implementation;

namespace WeatherApp.UI.ViewModels.Main.Implementation
{
    public class ItemsViewModel : IItemsViewModel
    {
        public string DateTimeText { get; set; }
        public string IconUrl { get; set; }
        public string Description { get; set; }
        public double Temp { get; set; }
        public double WindSpeed { get; set; }
    }
}
