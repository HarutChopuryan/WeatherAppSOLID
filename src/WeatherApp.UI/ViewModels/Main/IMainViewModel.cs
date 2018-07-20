using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using WeatherApp.Core.Models;
using WeatherApp.UI.ViewModels.Main.Implementation;

namespace WeatherApp.UI.ViewModels.Main
{
    public interface IMainViewModel : INotifyPropertyChanged
    {
        string City { get; set; }

        ICommand LoadCommand { get; }

        ICommand HeaderSelectedCommand { get; }

        bool ActivityIndicatorVisibility { get; set; }

        string ErrorMessage { get; set; }

        bool ErrorVisibility { get; set; }
        string SearchQuery { get; set; }

        bool FrameVisibility { get; set; }

        Weather Weather { get; set; }

        IList<Grouping<ItemsViewModel>> Items { get; set; }
    }
}