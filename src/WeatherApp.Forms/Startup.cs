using System;
using System.Collections.Generic;
using System.Text;
using Grace.DependencyInjection;

namespace WeatherApp.Forms
{
    public static class Startup
    {
        public static DependencyInjectionContainer RegisterFormsDependencies(this DependencyInjectionContainer container)
        {
            return container;
        }
    }
}