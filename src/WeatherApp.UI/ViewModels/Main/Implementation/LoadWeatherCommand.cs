using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using WeatherApp.Core.Models;
using WeatherApp.Core.Services;
using Xamarin.Forms;
using Command = WeatherApp.UI.ViewModels.Base.Implementation.Command;

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
        }

        public override async void Execute(object parameter)
        {
            if (_viewModel.Items != null && _viewModel.Items.Count > 0)
                _viewModel.Items.Clear();
            _viewModel.ErrorVisibility = false;
            _viewModel.ActivityIndicatorVisibility = true;
            string UppercaseFirst(string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return string.Empty;
                }
                return char.ToUpper(s[0]) + s.Substring(1);
            }
            string cityName = (string) parameter;
            cityName.ToLower();
            cityName = cityName.ToLower();
            cityName = cityName.Replace(" ", "");
            cityName = UppercaseFirst(cityName);
            try
            {
                _viewModel.Weather = await _weatherService.GetWeatherAsync(cityName);
            }
            catch (HttpRequestException)
            {
                _viewModel.ErrorVisibility = true;
                _viewModel.ErrorMessage = "No internet access";
                //await _viewModel.Navigation.NavigateToAsync<>()
                return;
            }
            catch (Exception)
            {
                _viewModel.ErrorVisibility = true;
                _viewModel.ErrorMessage = "City not found";
            }

            for (int i = 0; i < _viewModel.Weather.ListItems.Length; i++)
            {
                _viewModel.Weather.ListItems[i].MainItems.Temp -= 273.15;
            }
            _viewModel.Items = _viewModel.Weather.ListItems.GroupBy(item => DateTime.Parse(item.DateTimeText).ToString("yyyy-MM-dd"))
                    .Select(grouping => new Grouping<ListItem>(grouping.Key, grouping))
                    .ToList();
            _viewModel.ActivityIndicatorVisibility = false;
        }
    }
}
