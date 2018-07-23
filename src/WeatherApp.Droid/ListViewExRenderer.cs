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
        private int lastFirstVisibleItem = 0;
        public ListViewExRenderer(Context context) : base(context)
        {
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
        }

        public void OnScrollStateChanged(AbsListView view, ScrollState scrollState)
        {
            bool isScrollingUp = false;
            int currentFirstVisibleItem = view.FirstVisiblePosition;
            if (currentFirstVisibleItem > lastFirstVisibleItem)
            {
                isScrollingUp = false;
            }
            else if (currentFirstVisibleItem < lastFirstVisibleItem)
            {
                isScrollingUp = true;
            }
            lastFirstVisibleItem = currentFirstVisibleItem;
            if(isScrollingUp)
            {
                if (scrollState == ScrollState.Idle)
                {
                    if (Element != null)
                    {
                        var element = (MyListView)Element;
                        element.ExpandHeader();
                        element.ShrinkFooter();
                    }
                }
            }
            else
            {
                if (scrollState == ScrollState.Idle)
                {
                    if (Element != null)
                    {
                        var element = (MyListView)Element;
                        element.ShrinkHeader();
                        element.ExpandFooter();
                    }
                }
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (Element == null || Control == null)
                return;
            Control.SetOnScrollListener(this);
            Control.ScrollChange += Control_ScrollChange;
        }

        private void Control_ScrollChange(object sender, ScrollChangeEventArgs e)
        {
        }
    }
}