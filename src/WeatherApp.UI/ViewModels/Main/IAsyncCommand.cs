using System.Threading;
using System.Threading.Tasks;

namespace WeatherApp.UI.ViewModels.Main
{
    interface IAsyncCommand : IBindableCommand
    {
        bool IsBusy { get; }

        string FailureMessage { get; }

        bool IsSuccessful { get; }

        Task ExecuteAsync(object param, CancellationToken token = default(CancellationToken));
    }
}
