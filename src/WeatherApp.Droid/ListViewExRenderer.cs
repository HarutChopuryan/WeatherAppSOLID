using System;
using Android.Content;
using Android.Util;
using Android.Widget;
using WeatherApp.Droid;
using WeatherApp.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Xamarin.Forms.ListView;

[assembly: ExportRenderer(typeof(MyListView), typeof(ListViewExRenderer))]

namespace WeatherApp.Droid
{
    public class ListViewExRenderer : ListViewRenderer, AbsListView.IOnScrollListener
    {
        private readonly Context _context;
        private int firstVisibleItem = -1;
        private int scrolledCount = 0;

        public ListViewExRenderer(Context context) : base(context)
        {
            _context = context;
            ScrollChange += OnScrollChange;
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
        }

        public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
        {
            if (Element != null)
            {
                var element = (MyListView) Element;
                if (firstVisibleItem == -1)
                    firstVisibleItem = view.FirstVisiblePosition;
                var currentFirstVisibleItem = -1;
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
                        var c = Control.GetChildAt(0);
                        element.FirstVisibleItemTopYOffsetAfterScroll = ConvertPixelsToDp(Math.Abs(c.Top));
                        element.FirstVisibleItemAfterScroll = Control.FirstVisiblePosition;
                        element.CellHeight = ConvertPixelsToDp(c.Height + c.PaddingBottom + c.PaddingTop);
                        element.SelectionRepositioning();
                    }
                    firstVisibleItem = currentFirstVisibleItem;
                }
            }
        }

        private void OnScrollChange(object sender, ScrollChangeEventArgs e)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (Element == null || Control == null)
                return;
            Control.SetOnScrollListener(this);
        }


        public float ConvertDpToPixel(float dp)
        {
            var resources = _context.Resources;
            var metrics = resources.DisplayMetrics;
            var px = dp * ((float) metrics.DensityDpi / (int) DisplayMetricsDensity.Default);
            return px;
        }

        public float ConvertPixelsToDp(float px)
        {
            var resources = _context.Resources;
            var metrics = resources.DisplayMetrics;
            var dp = px / ((float) metrics.DensityDpi / (int) DisplayMetricsDensity.Default);
            return dp;
        }
    }
}