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
        private readonly MainViewModel _viewModel;
        private readonly IWeatherService _weatherService;

        public LoadWeatherCommand(MainViewModel viewModel, IWeatherService weatherService)
        {
            _viewModel = viewModel;
            _weatherService = weatherService;
            _viewModel.Items = new ObservableCollection<Grouping<ListItem>>();
            //_viewModel.GItems = new ObservableCollection<ParentList>();
        }

        public override async void Execute(object parameter)
        {
            SetToNullItemsContent();
            _viewModel.ErrorVisibility = false;
            _viewModel.ActivityIndicatorVisibility = true;
            var cityName = (string) parameter;
            if (string.IsNullOrWhiteSpace(cityName))
            {
                _viewModel.FrameVisibility = false;
                _viewModel.ErrorVisibility = true;
                SetToNullItemsContent();
                _viewModel.ErrorMessage = "Type city name";
                _viewModel.ActivityIndicatorVisibility = false;
                return;
            }
            cityName = cityName.ToLower();
            cityName = MakeUpperFirstLetter(cityName);
            try
            {
                var currentState = Connectivity.NetworkAccess;
                if (currentState == NetworkAccess.Internet)
                {
                    _viewModel.Weather = await _weatherService.GetWeatherAsync(cityName);
                    foreach (var item in _viewModel.Weather.ListItems)
                        item.MainItems.Temp -= 273.15;

                    _viewModel.Items = _viewModel.Weather.ListItems
                        .GroupBy(item => DateTime.Parse(item.DateTimeText).ToString("yyyy-MM-dd"))
                        .Select(grouping => new Grouping<ListItem>(grouping.Key, grouping.Select(listItem =>
                        {
                            listItem.DateTimeText = listItem.DateTimeText.Substring(10);
                            return listItem;
                        })))
                        .ToList();
                    _viewModel.FrameVisibility = true;
                }
                else
                {
                    _viewModel.FrameVisibility = false;
                    _viewModel.ActivityIndicatorVisibility = true;
                    _viewModel.ErrorVisibility = true;
                    SetToNullItemsContent();
                    _viewModel.ErrorMessage = "No internet access";
                    _viewModel.ActivityIndicatorVisibility = false;
                }
            }
            catch (HttpRequestException)
            {
                _viewModel.FrameVisibility = false;
                _viewModel.ActivityIndicatorVisibility = true;
                _viewModel.ErrorVisibility = true;
                SetToNullItemsContent();
                _viewModel.ErrorMessage = "No internet access";
            }
            catch (NullReferenceException)
            {
                _viewModel.FrameVisibility = false;
                _viewModel.ActivityIndicatorVisibility = true;
                _viewModel.ErrorVisibility = true;
                SetToNullItemsContent();
                _viewModel.ErrorMessage = "City not found";
            }
            finally
            {
                _viewModel.ActivityIndicatorVisibility = false;
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
            if (_viewModel.Items != null && _viewModel.Items.Count > 0)
                _viewModel.Items = null;
        }
    }
}