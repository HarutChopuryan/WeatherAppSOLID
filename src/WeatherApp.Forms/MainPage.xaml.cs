using System;
using System.Collections.Generic;
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

	    async void OnItemTapped(object sender, SelectedItemChangedEventArgs e)
	    {
            listView.ScrollTo(listView.SelectedItem, ScrollToPosition.Center, true);
	        listView.SelectedItem = null;
	    }

	    protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() => _entry.Focus());
        }
    }
}