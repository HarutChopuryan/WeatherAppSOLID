using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Grace.DependencyInjection;

namespace WeatherApp.Droid
{
    public static class Startup
    {
        public static DependencyInjectionContainer RegisterDroidDependencies(this DependencyInjectionContainer container)
        {
            return container;
        }
    }
}