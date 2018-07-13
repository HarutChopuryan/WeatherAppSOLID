using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using PropertyChanged;
using WeatherApp.Core.Models;
using WeatherApp.Core.Services;
using WeatherApp.UI.ViewModels.Base.Implementation;

namespace WeatherApp.UI.ViewModels.Main.Implementation
{
    [AddINotifyPropertyChangedInterface]
    internal class MainViewModel : BaseBindableObject, IMainViewModel
    {
        public MainViewModel(IWeatherService weatherService)
        {
            LoadCommand = new LoadWeatherCommand(this, weatherService);
        }
        public string City { get; set; }
        public ICommand LoadCommand { get; }
        public Weather Weather { get; internal set; }
        public List<Grouping<ListItem>> Items { get; set; }
    }
}