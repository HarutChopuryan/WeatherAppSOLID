using System.Collections.Generic;
using System.ComponentModel;

namespace WeatherApp.Core.Models
{
    public class WeatherGroup : List<ListItem>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private WeatherGroup(string headerText)
        {
            this.HeaderText = headerText;
        }

        public string HeaderText { get; set; }

        static WeatherGroup()
        {
            //Client client = new Client();
        }

        public Weather _weather;
        public Weather Weather
        {
            set
            {
                if (_weather != value)
                {
                    _weather = value;
                    OnPropertyChanged(nameof(Weather));
                }
            }
            get
            {
                return _weather;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
