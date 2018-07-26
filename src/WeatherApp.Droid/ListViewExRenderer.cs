using Android.Content;
using Android.Util;
using Android.Widget;
using Java.Lang;
using WeatherApp.Droid;
using WeatherApp.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Essentials;
using ListView = Xamarin.Forms.ListView;

[assembly: ExportRenderer(typeof(MyListView), typeof(ListViewExRenderer))]

namespace WeatherApp.Droid
{
    public class ListViewExRenderer : ListViewRenderer, AbsListView.IOnScrollListener
    {
        private int firstVisibleItem = -1;
        private int scrolledCount = 0;

        public ListViewExRenderer(Context context) : base(context)
        {
            ScrollChange += OnScrollChange;
        }

        private void OnScrollChange(object sender, ScrollChangeEventArgs e)
        {
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
        }

        public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
        {
            if (Element != null)
            {
                var element = (MyListView)Element;
                if (firstVisibleItem == -1)
                    firstVisibleItem = view.FirstVisiblePosition;
                int currentFirstVisibleItem = -1;
                if (view.CanScrollVertically(-1))
                    element.ShrinkFooter();
                if (view.CanScrollVertically(1))
                    element.ShrinkHeader();
                if (!view.CanScrollVertically(-1))
                {
                    element.ShrinkFooter();
                    element.ExpandHeader();
                }
                if (!view.CanScrollVertically(1))
                {
                    element.ShrinkHeader();
                    element.ExpandFooter();
                }
                if (scrollState == ScrollState.Idle)
                {
                    if (Element != null)
                    {
                        var metrics = DeviceDisplay.ScreenMetrics;
                        var c = Control.GetChildAt(0);
                        element.FirstVisibleItemTopYOffsetAfterScroll = -c.Top / metrics.Density;
                        element.FirstVisibleItemAfterScroll = Control.FirstVisiblePosition;
                        element.SelectionRepositioning();
                    }
                    firstVisibleItem = currentFirstVisibleItem;
                }
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (Element == null || Control == null)
                return;
            Control.SetOnScrollListener(this);
        }
    }
}