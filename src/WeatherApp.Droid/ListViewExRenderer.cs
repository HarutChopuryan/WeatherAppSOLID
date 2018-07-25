using Android.Content;
using Android.Util;
using Android.Widget;
using Java.Lang;
using WeatherApp.Droid;
using WeatherApp.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Xamarin.Forms.ListView;

[assembly: ExportRenderer(typeof(MyListView), typeof(ListViewExRenderer))]

namespace WeatherApp.Droid
{
    public class ListViewExRenderer : ListViewRenderer, AbsListView.IOnScrollListener
    {
        private int firstVisibleItem = -1;
        private int scrolledCount = 0;
        private readonly Context _context;

        public ListViewExRenderer(Context context) : base(context)
        {
            _context = context;
            this.ScrollChange += OnScrollChange;
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
                    //currentFirstVisibleItem = view.FirstVisiblePosition;
                    //scrolledCount += currentFirstVisibleItem - firstVisibleItem;
                    //if (scrolledCount != 0)
                    //{
                    //    if (scrolledCount != 1)
                    //    {
                    //        if (Control.GetChildAt(0).Top % scrolledCount != 0)
                    //            --scrolledCount;
                    //    }
                    //    else
                    //    {
                    //        --scrolledCount;
                    //    }
                    //}
                    if (Element != null)
                    {
                        var c = Control.GetChildAt(0);
                        element.FirstVisibleItemTopYOffsetAfterScroll = -c.Top;
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

        public float ConvertPixelsToDp(float px)
        {
            DisplayMetrics metrics = _context.Resources.DisplayMetrics;
            float dp = px / ((int)metrics.DensityDpi / 160f);
            return Math.Round(dp);
        }
    }
}