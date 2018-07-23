using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using WeatherApp.UI.ViewModels.Main;
using WeatherApp.UI.ViewModels.Main.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Forms
{
    public partial class MainPage : ContentPage
    {
        private readonly IMainViewModel _viewModel;
        public MainPage(IMainViewModel viewModel)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            _viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
            //WeatherlistView.ScrollToRequested += WeatherlistView_ScrollToRequested;
            WeatherlistView.OnExpandHeader += WeatherlistView_OnExpandHeader;
            WeatherlistView.OnExpandFooter += WeatherlistView_OnExpandFooter;
            WeatherlistView.OnShrinkHeader += WeatherlistView_OnShrinkHeader;
            WeatherlistView.OnShrinkFooter += WeatherlistView_OnShrinkFooter;
        }

        private void WeatherlistView_OnShrinkFooter(object sender, EventArgs e)
        {
            
        }

        private void WeatherlistView_OnShrinkHeader(object sender, EventArgs e)
        {
            topSpace.IsVisible = false;
        }

        private void WeatherlistView_OnExpandFooter(object sender, EventArgs e)
        {
            
        }

        private void WeatherlistView_OnExpandHeader(object sender, EventArgs e)
        {
            topSpace.IsVisible = true;
            int topCount = 0;
            int selectedTopItemIndex = -1;
            var selectedItem = (WeatherlistView.ItemsSource as List<Grouping<ItemsViewModel>>).Select(item => new SelectedItemGroup()
            {
                Key = item.Key,
                Index = item.IndexOf(WeatherlistView.SelectedItem),
                Count = item.Count
            });
            //foreach (var item in selectedItem)
            //{
            //    topCount = item.Count;
            //    break;
            //}
            foreach (var item in selectedItem)
            {
                if (item.Key == DateTime.Now.ToString("yyyy-MM-dd") && item.Index != -1)
                {
                    selectedTopItemIndex = item.Index;
                    topCount = item.Count;
                }
            }
            for (int i = 0; i < topCount; i++)
            {
                if (selectedTopItemIndex <= (topCount / 2) && selectedTopItemIndex == i)
                {
                    _viewModel.TopSpaceVisibility = true;
                    topSpace.HeightRequest = (topCount / 2 == 0)
                        ? 70 * ((topCount / 2) + 1 - selectedTopItemIndex)
                        : 70 * ((topCount / 2) + 2 - selectedTopItemIndex);
                    WeatherlistView.SelectedItem = null;
                    return;
                }
            }
            WeatherlistView.SelectedItem = null;
        }

        //private void WeatherlistView_ScrollToRequested(object sender, ScrollToRequestedEventArgs e)
        //{

        //}

        void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            
            //int bottomCount = 0;
            //int selectedBottomItemIndex = -1;
            //DateTime after5Days = DateTime.Today.AddDays(5);
            ////foreach (var item in selectedItem)
            ////{
            ////    if (item.Key == DateTime.Now.ToString("yyyy-MM-dd") && item.Index != -1)
            ////        selectedTopItemIndex = item.Index;
            ////    if (item.Key == after5Days.ToString("yyyy-MM-dd") && item.Index != -1)
            ////    {
            ////        selectedBottomItemIndex = item.Index;
            ////        bottomCount = item.Count;
            ////    }
            ////}
            //for (int i = bottomCount; i > 0; i--)
            //{
            //    if (selectedBottomItemIndex >= (bottomCount / 2) && selectedBottomItemIndex == i)
            //    {
            //        _viewModel.BottomSpaceVisibility = true;
            //        bottomSpace.HeightRequest = (bottomCount / 2 == 0)
            //            ? 70 * ((bottomCount / 2) + 1 - selectedBottomItemIndex)
            //            : 70 * ((bottomCount / 2) + 2 - selectedBottomItemIndex);
            //        WeatherlistView.SelectedItem = null;
            //        return;
            //    }
            //}
            WeatherlistView.ScrollTo(WeatherlistView.SelectedItem, ScrollToPosition.Center, true);
            WeatherlistView.SelectedItem = null;
        }
    }
}