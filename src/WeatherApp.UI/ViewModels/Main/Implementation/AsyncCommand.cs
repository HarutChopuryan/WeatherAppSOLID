using PropertyChanged;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.UI.ViewModels.Base.Implementation;

namespace WeatherApp.UI.ViewModels.Main.Implementation
{
    [AddINotifyPropertyChangedInterface]
    public abstract class AsyncCommand : BaseBindableCommand, IAsyncCommand
    {
        public AsyncCommand() : base(true)
        {
        }

        public AsyncCommand(bool canExecute) : base(canExecute)
        {
        }

        public override async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public bool IsBusy { get; protected set; }
        public string FailureMessage { get; protected set; }
        public bool IsSuccessful { get; protected set; }
        
        public virtual async Task ExecuteAsync(object parameter, CancellationToken token = default(CancellationToken))
        {
            if (IsBusy)
                return;
            IsBusy = true;
            IsSuccessful = false;
            FailureMessage = "";
            try
            {
                IsSuccessful = await ExecuteCoreAsync(parameter, token);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        
        protected abstract Task<bool> ExecuteCoreAsync(object parameter, CancellationToken token = default(CancellationToken));
        
        protected virtual void HandleException(Exception exception)
        {
            FailureMessage = exception.Message;
            Debug.WriteLine(exception);
        }
    }
}
