using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WeatherApp.Core.Models;
using WeatherApp.UI.ViewModels.Main.Implementation;

namespace WeatherApp.UI.ViewModels.Main
{
    public interface IItemsViewModel
    {
        string DateTimeText { get; set; }

        string IconUrl { get; set; }

        string Description { get; set; }

        double Temp { get; set; }

        double WindSpeed { get; set; }
    }
}