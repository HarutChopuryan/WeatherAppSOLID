using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WeatherApp.Core.Models;
using WeatherApp.Core.Services;
using WeatherApp.UI.ViewModels.Base.Implementation;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WeatherApp.UI.ViewModels.Main.Implementation
{
    internal class LoadWeatherCommand : AsyncCommand /*Command*/
    {
        private readonly MainViewModel _mainViewModel;
        private readonly IWeatherService _weatherService;

        public LoadWeatherCommand(MainViewModel mainViewModel, IWeatherService weatherService)
        {
            _mainViewModel = mainViewModel;
            _weatherService = weatherService;
            _mainViewModel.Items = new ObservableCollection<Grouping<ItemsViewModel>>();
        }

        protected override Task<bool> ExecuteCoreAsync(object parameter, CancellationToken token = default(CancellationToken))
        {
            SetToNullItemsContent();
            var cityName = (string)parameter;
            if (string.IsNullOrWhiteSpace(cityName))
            {
                SetToNullItemsContent();
                FailureMessage = "Type city name";
                return
                    Task.FromResult(false);
            }

            cityName = cityName.ToLower();
            cityName = MakeUpperFirstLetter(cityName);
            var currentState = Connectivity.NetworkAccess;
            if (currentState == NetworkAccess.Internet)
            {
                _mainViewModel.Weather = _weatherService.GetWeatherAsync(cityName).Result;

                _mainViewModel.Items = _mainViewModel.Weather.ListItems
                    .GroupBy(item => DateTime.Parse(item.DateTimeText).ToString("yyyy-MM-dd"))
                    .Select(grouping => new Grouping<ItemsViewModel>(grouping.Key, grouping.Select(MapListItemToItemsViewModel)))
                    .ToList();

                _mainViewModel.FlattenedItems = new List<object>();
                foreach (var group in _mainViewModel.Items)
                {
                    _mainViewModel.FlattenedItems.Add(group);
                    foreach (var itemsViewModel in group)
                    {
                        _mainViewModel.FlattenedItems.Add(itemsViewModel);
                    }
                }
                
                return
                    Task.FromResult(true);
            }
            else
            {
                SetToNullItemsContent();
                IsBusy = false;
                IsSuccessful = false;
                FailureMessage = "No internet access";
                return
                    Task.FromResult(false);
            }
        }

        protected override void HandleException(Exception exception)
        {
            if (exception is HttpRequestException)
            {
                SetToNullItemsContent();
                IsBusy = false;
                IsSuccessful = false;
                FailureMessage = "No internet access";
                return;
            }
            else if (exception is NullReferenceException)
            {
                SetToNullItemsContent();
                IsBusy = false;
                IsSuccessful = false;
                FailureMessage = "City not found";
                return;
            }
            else
            {
                SetToNullItemsContent();
                IsBusy = false;
                IsSuccessful = false;
                FailureMessage = "City not found";
                return;
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

        private ItemsViewModel MapListItemToItemsViewModel(ListItem listItem)
        {
            return new ItemsViewModel
            {
                DateTimeText = listItem.DateTimeText.Substring(10, 6),
                Temp = $"{listItem.MainItems.Temp - 273.15:0.0} \u00B0C",
                Description = listItem.WeatherItems[0].Description,
                IconUrl = listItem.WeatherItems[0].Description.Replace(" ", "_") + ".png",
                WindSpeed = $"{listItem.Wind.Speed:0.0} m/s"
            };
        }
    }
}