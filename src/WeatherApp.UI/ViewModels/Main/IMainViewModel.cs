using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WeatherApp.Core.Models;
using WeatherApp.UI.ViewModels.Main.Implementation;

namespace WeatherApp.UI.ViewModels.Main
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        string City { get; set; }
        ICommand LoadCommand { get; }
        Core.Models.Weather Weather { get; }
        List<Grouping<ListItem>> Items { get; set; }
    }
}
