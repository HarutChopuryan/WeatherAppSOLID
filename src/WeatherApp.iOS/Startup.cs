using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Grace.DependencyInjection;
using UIKit;

namespace WeatherApp.iOS
{
    public static class Startup
    {
        public static DependencyInjectionContainer RegisteriOSDependencies(this DependencyInjectionContainer container)
        {
            return container;
        }
    }
}