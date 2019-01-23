using System.ComponentModel;
using System.Windows.Input;

namespace WeatherApp.UI.ViewModels.Main
{
    interface IBindableCommand : ICommand, INotifyPropertyChanged
    {
        bool IsExecutable { get; }
    }
}
