using PropertyChanged;
using System;
using WeatherApp.UI.ViewModels.Main;

namespace WeatherApp.UI.ViewModels.Base.Implementation
{
    [AddINotifyPropertyChangedInterface]
    public abstract class BaseBindableCommand : BaseBindableObject, IBindableCommand
    {
        private bool _canExecute;

        public event EventHandler CanExecuteChanged;

        protected BaseBindableCommand(bool canExecute = true)
        {
            _canExecute = canExecute;
            IsExecutable = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public abstract void Execute(object parameter);
        
        protected void SetCanExecute(bool canExecute)
        {
            _canExecute = IsExecutable = canExecute;
            EventHandler canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged == null)
                return;
            EventArgs empty = EventArgs.Empty;
            canExecuteChanged(this, empty);
        }

        public bool IsExecutable { get; private set; }
    }
}
