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
            WeatherlistView.OnExpandHeader += WeatherlistView_OnExpandHeader;
            WeatherlistView.OnExpandFooter += WeatherlistView_OnExpandFooter;
            WeatherlistView.OnShrinkHeader += WeatherlistView_OnShrinkHeader;
            WeatherlistView.OnShrinkFooter += WeatherlistView_OnShrinkFooter;
            WeatherlistView.OnSelectionRepositioning += WeatherlistView_OnSelectionRepositioning;
        }

        private void WeatherlistView_OnSelectionRepositioning(object sender, EventArgs e)
        {
            //Debug.WriteLine(WeatherlistView.ScrollY);
            
            double selectionCenter = (selection.Y + selection.Y + bottomOfSelection.Y) / 2;
            //double prevItemCenter = selectionCenter - (selection.Height / 2) - WeatherlistView.RowHeight / 2;
            //double nextItemCenter = selectionCenter + (selection.Height / 2) + WeatherlistView.RowHeight / 2;
            //if (selectionCenter - prevItemCenter < nextItemCenter - selectionCenter)
            //    WeatherlistView.ScrollTo(prevItemCenter, ScrollToPosition.MakeVisible, true);
            //else
            //    WeatherlistView.ScrollTo(nextItemCenter, ScrollToPosition.MakeVisible, true);
        }

        private void WeatherlistView_OnShrinkFooter(object sender, EventArgs e)
        {
            bottomSpace.IsVisible = false;
        }

        private void WeatherlistView_OnShrinkHeader(object sender, EventArgs e)
        {
            topSpace.IsVisible = false;
        }

        private void WeatherlistView_OnExpandFooter(object sender, EventArgs e)
        {
            bottomSpace.IsVisible = true;
            bottomSpace.HeightRequest = WeatherlistView.Height / 2 - 70;
        }

        private void WeatherlistView_OnExpandHeader(object sender, EventArgs e)
        {
            topSpace.IsVisible = true;
            topSpace.HeightRequest = WeatherlistView.Height / 2 - 70;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            WeatherlistView.ScrollTo(WeatherlistView.SelectedItem, ScrollToPosition.Center, true);
            WeatherlistView.SelectedItem = null;
        }
    }
}