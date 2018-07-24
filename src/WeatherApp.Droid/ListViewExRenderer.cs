using Android.Content;
using Android.Widget;
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
        public ListViewExRenderer(Context context) : base(context)
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