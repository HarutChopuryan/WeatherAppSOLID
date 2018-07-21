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
		}

	    void OnItemTapped(object sender, ItemTappedEventArgs itemTappedEventArgs)
	    {
            int topCount = 0;
	        int bottomCount = 0;
            int selectedTopItemIndex = -1;
	        int selectedBottomItemIndex = -1;
            var selectedItem = (listView.ItemsSource as List<Grouping<ItemsViewModel>>).Select(item => new SelectedItemGroup()
            {
                Key = item.Key,
                Index = item.IndexOf(listView.SelectedItem),
                Count = item.Count
            });
            foreach (var item in selectedItem)
            {
                topCount = item.Count;
                break;
            }
	        DateTime after5Days = DateTime.Today.AddDays(5);
            foreach (var item in selectedItem)
            {
                if (item.Key == DateTime.Now.ToString("yyyy-MM-dd") && item.Index != -1)
                    selectedTopItemIndex = item.Index;
                if (item.Key == after5Days.ToString("yyyy-MM-dd") && item.Index != -1)
                {
                    selectedBottomItemIndex = item.Index;
                    bottomCount = item.Count;
                }
            }

	        for (int i = 0; i < topCount; i++)
	        {
	            if (selectedTopItemIndex == i)
	            {
	                _viewModel.TopSpaceVisibility = true;
                    topSpace.HeightRequest = (topCount / 2 == 0)
                        ? 70 * ((topCount / 2) + 1 - selectedTopItemIndex)
                        : 70 * ((topCount / 2) + 2 - selectedTopItemIndex);
                    listView.SelectedItem = null;
                    return;
                }
	        }
            for (int i = bottomCount; i > 0; i--)
            {
                if (selectedBottomItemIndex >= (bottomCount / 2) && selectedBottomItemIndex == i)
                {
                    _viewModel.BottomSpaceVisibility = true;
                    //double bottomMargin = (bottomCount / 2 == 0)
                    //    ? 70 * ((bottomCount / 2) + 1 - selectedBottomItemIndex)
                    //    : 70 * ((bottomCount / 2) + 2 - selectedBottomItemIndex);
                    bottomSpace.HeightRequest = (bottomCount / 2 == 0)
                        ? 70 * ((bottomCount / 2) + 1 - selectedBottomItemIndex)
                        : 70 * ((bottomCount / 2) + 2 - selectedBottomItemIndex);
                    //bottomSpace.Margin = new Thickness(0, 0, 0, bottomMargin);
                    //listView.ScrollTo(listView.SelectedItem, ScrollToPosition.Center, true);
                    //listViewStackLayout.Margin = new Thickness(0, 0, 0, bottomMargin);
                    listView.SelectedItem = null;
                    return;
                }
            }
            _viewModel.TopSpaceVisibility = false;
	        _viewModel.BottomSpaceVisibility = false;
	        listView.ScrollTo(listView.SelectedItem, ScrollToPosition.Center, true);
	        listView.SelectedItem = null;
        }
	}
}