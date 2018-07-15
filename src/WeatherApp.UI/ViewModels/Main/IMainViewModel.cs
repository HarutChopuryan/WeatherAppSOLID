
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Xml.XPath;
using WeatherApp.Core.Models;
using WeatherApp.UI.ViewModels.Main.Implementation;
using Xamarin.Forms;

namespace WeatherApp.UI.ViewModels.Main
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        string City { get; set; }
        ICommand LoadCommand { get; }
        Core.Models.Weather Weather { get; }
        List<Grouping<ListItem>> Items { get; set; }
        bool ActivityIndicatorVisibility { get; set; }
        string ErrorMessage { get; set; }
        bool ErrorVisibility { get; set; }
        INavigationService Navigation { get; set; }
    }
}
