using System;
using Xamarin.Forms;
namespace WeatherApp.Forms
{
    public class MyListView : ListView
    {
        public int FirstVisibleItemAfterScroll { get; set; }
        public int FirstVisibleItemTopYOffsetAfterScroll { get; set; }

        public event EventHandler OnExpandHeader;
        public event EventHandler OnShrinkHeader;

        public event EventHandler OnExpandFooter;
        public event EventHandler OnShrinkFooter;

        public event EventHandler OnSelectionRepositioning;

        public void ExpandHeader()
        {
            OnExpandHeader?.Invoke(this, null);
        }

        public void ExpandFooter()
        {
            OnExpandFooter?.Invoke(this, null);
        }

        public void ShrinkHeader()
        {
            OnShrinkHeader?.Invoke(this, null);
        }

        public void ShrinkFooter()
        {
            OnShrinkFooter?.Invoke(this, null);
        }

        public void SelectionRepositioning()
        {
            OnSelectionRepositioning?.Invoke(this, null);
        }
    }
}