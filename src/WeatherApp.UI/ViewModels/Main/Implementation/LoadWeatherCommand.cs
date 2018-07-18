using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using WeatherApp.Core.Models;
using WeatherApp.Core.Services;
using WeatherApp.UI.ViewModels.Base.Implementation;
using Xamarin.Essentials;

namespace WeatherApp.UI.ViewModels.Main.Implementation
{
    internal class LoadWeatherCommand : Command
    {
        private const string BaseImgUri = "http://openweathermap.org/img/w/";
        private readonly MainViewModel _mainViewModel;
        private readonly IWeatherService _weatherService;

        public LoadWeatherCommand(MainViewModel mainViewModel, IWeatherService weatherService)
        {
            _mainViewModel = mainViewModel;
            _weatherService = weatherService;
            _mainViewModel.Items = new ObservableCollection<ItemsViewModel>();
            //_mainViewModel.GItems = new ObservableCollection<ParentList>();
        }

        public override async void Execute(object parameter)
        {
            SetToNullItemsContent();
            _mainViewModel.ErrorVisibility = false;
            _mainViewModel.ActivityIndicatorVisibility = true;
            var cityName = (string) parameter;
            if (string.IsNullOrWhiteSpace(cityName))
            {
                _mainViewModel.FrameVisibility = false;
                _mainViewModel.ErrorVisibility = true;
                SetToNullItemsContent();
                _mainViewModel.ErrorMessage = "Type city name";
                _mainViewModel.ActivityIndicatorVisibility = false;
                return;
            }

            cityName = cityName.ToLower();
            cityName = MakeUpperFirstLetter(cityName);
            try
            {
                var currentState = Connectivity.NetworkAccess;
                if (currentState == NetworkAccess.Internet)
                {
                    _mainViewModel.Weather = await _weatherService.GetWeatherAsync(cityName);
                    foreach (var item in _mainViewModel.Weather.ListItems)
                        _mainViewModel.ListItemViewModel.Temp.Add(item.MainItems.Temp - 273.15);

                    foreach (var path in _mainViewModel.Weather.ListItems)
                        _mainViewModel.ListItemViewModel.IconUrl.Add(BaseImgUri + path.WeatherItems[0].Icon + ".png");

                    _mainViewModel.Items = _mainViewModel.Weather.ListItems
                        .GroupBy(item => DateTime.Parse(item.DateTimeText).ToString("yyyy-MM-dd"))
                        .Select(grouping => new Grouping<ListItem>(grouping.Key, grouping.Select(listItem =>
                        {
                            listItem.DateTimeText = listItem.DateTimeText.Substring(10);
                            return listItem;
                        })))
                        .ToList();

                    foreach (var item in _mainViewModel.Items)
                    {
                        _mainViewModel.ListItemViewModel.DateTimeText.Add(item.Key);
                        foreach (var description in item)
                        {
                            _mainViewModel.ListItemViewModel.Description.Add(description.WeatherItems[0].Description);
                            _mainViewModel.ListItemViewModel.WindSpeed.Add(description.Wind.Speed);
                        }
                    }
                    _mainViewModel.FrameVisibility = true;
                }
                else
                {
                    _mainViewModel.FrameVisibility = false;
                    _mainViewModel.ActivityIndicatorVisibility = true;
                    _mainViewModel.ErrorVisibility = true;
                    SetToNullItemsContent();
                    _mainViewModel.ErrorMessage = "No internet access";
                    _mainViewModel.ActivityIndicatorVisibility = false;
                }
            }
            catch (HttpRequestException)
            {
                _mainViewModel.FrameVisibility = false;
                _mainViewModel.ActivityIndicatorVisibility = true;
                _mainViewModel.ErrorVisibility = true;
                SetToNullItemsContent();
                _mainViewModel.ErrorMessage = "No internet access";
            }
            catch (NullReferenceException)
            {
                _mainViewModel.FrameVisibility = false;
                _mainViewModel.ActivityIndicatorVisibility = true;
                _mainViewModel.ErrorVisibility = true;
                SetToNullItemsContent();
                _mainViewModel.ErrorMessage = "City not found";
            }
            finally
            {
                _mainViewModel.ActivityIndicatorVisibility = false;
            }
        }

        private string MakeUpperFirstLetter(string s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        private void SetToNullItemsContent()
        {
            if (_mainViewModel.Items != null && _mainViewModel.Items.Count > 0)
                _mainViewModel.Items = null;
        }
    }
}