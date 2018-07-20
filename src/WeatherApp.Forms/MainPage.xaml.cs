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
	        //double selectionX = 0;
	        //double selectionY = 0;
         //   int count = 0;
	        //int selectedItemIndex = -1;
	        //int selectedItemOldIndex = -1;
         //   var selectedItem = (listView.ItemsSource as List<Grouping<ItemsViewModel>>).Select(item=> new SelectedItemGroup()
         //   {
         //       Key = item.Key,
         //       Index = item.IndexOf(listView.SelectedItem),
         //       Count = item.Count
         //   });
	        //foreach (var item in selectedItem)
	        //{
	        //    count = item.Count;
	        //    selectedItemOldIndex = item.Index;
         //       break;
	        //}
	        //foreach (var item in selectedItem)
	        //{
	        //    if (item.Key == DateTime.Today.ToString("yyyy-MM-dd") && item.Index != -1)
	        //        selectedItemIndex = item.Index;
	        //}

	        //for (int i = 0; i < count; i++)
	        //{
	        //    if (selectedItemIndex == i)
	        //    {
	        //        if (selectedItemIndex < selectedItemOldIndex)
	        //            selection.TranslateTo(0, +60 * (count - i), 500);
	        //        else if(selectedItemIndex==count-1)
	        //            selection.TranslateTo(0, -47 * (count - i), 500);
	        //        else
	        //            selection.TranslateTo(0, -60 * (count - i), 500);
         //       }
	        //}
            listView.ScrollTo(listView.SelectedItem, ScrollToPosition.Center, true);
	        listView.SelectedItem = null;
	    }
	}
}