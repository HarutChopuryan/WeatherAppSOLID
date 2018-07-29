using System;
using WeatherApp.UI.ViewModels.Main;
using WeatherApp.UI.ViewModels.Main.Implementation;
using Xamarin.Forms;

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
            if (WeatherlistView.FirstVisibleItemAfterScroll != 0)
            {
                var scrollY = (WeatherlistView.FirstVisibleItemAfterScroll - 1) * WeatherlistView.CellHeight +
                              WeatherlistView.FirstVisibleItemTopYOffsetAfterScroll;
                var scrollYAtCenter = scrollY + (selection.Y - WeatherlistView.Y);
                var estimatedItemAtCenter = scrollYAtCenter / WeatherlistView.CellHeight;
                var itemToCenterIndex = Math.Round(estimatedItemAtCenter);
                var itemToCenter = _viewModel.FlattenedItems[(int) itemToCenterIndex];
                if (itemToCenter is Grouping<ItemsViewModel> group) itemToCenter = group[0];
                WeatherlistView.ScrollTo(itemToCenter, ScrollToPosition.Center, true);
            }
            else
            {
                var estimatedItemAtCenter = (selection.Y - _entry.Height - WeatherlistView.TopSpaceScrollY) / bottomOfSelection.Y;
                var itemToCenterIndex = Math.Round(estimatedItemAtCenter);
                var itemToCenter = _viewModel.FlattenedItems[(int)itemToCenterIndex];
                if (itemToCenter is Grouping<ItemsViewModel> group) itemToCenter = group[0];
                WeatherlistView.ScrollTo(itemToCenter, ScrollToPosition.Center, true);
            }
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
            bottomSpace.HeightRequest = WeatherlistView.Height / 2;
        }

        private void WeatherlistView_OnExpandHeader(object sender, EventArgs e)
        {
            topSpace.IsVisible = true;
            topSpace.HeightRequest = WeatherlistView.Height / 2 - _entry.Height;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
        {
            WeatherlistView.ScrollTo(WeatherlistView.SelectedItem, ScrollToPosition.Center, true);
            WeatherlistView.SelectedItem = null;
        }
    }
}