using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WeatherApp.Core.Models;
using WeatherApp.UI.ViewModels.Main.Implementation;

namespace WeatherApp.UI.ViewModels.Base.Implementation
{
    public abstract class Command : ICommand
    {
        private bool _canExecute;

        public event EventHandler CanExecuteChanged;

        protected Command(bool canExecute = true)
        {
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public abstract void Execute(object parameter = null);

        protected void SetCanExecute(bool canExecute)
        {
            _canExecute = canExecute;
            EventHandler canExecuteChanged = CanExecuteChanged;
            if (canExecuteChanged == null)
                return;
            EventArgs empty = EventArgs.Empty;
            canExecuteChanged(this, empty);
        }
    }
}
