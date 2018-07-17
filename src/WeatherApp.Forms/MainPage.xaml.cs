using System;
using System.Runtime.Serialization.Formatters.Binary;
using WeatherApp.UI.ViewModels.Main;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherApp.Forms
{
	public partial class MainPage : ContentPage
	{
	    private readonly IMainViewModel _viewModel;
		public MainPage(IMainViewModel viewModel)
		{
		    _viewModel = viewModel;
		    InitializeComponent();
		    BindingContext = _viewModel;
		}

	    void OnItemTapped(object sender, SelectedItemChangedEventArgs e)
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
