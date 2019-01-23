using System;
using System.Collections.Generic;
using System.Text;
using Grace.DependencyInjection;
using Grace.DependencyInjection.Lifestyle;
using WeatherApp.Core.Services;
using WeatherApp.Core.Services.Implementation;

namespace WeatherApp.Core
{
    public static class Startup
    {
        public static DependencyInjectionContainer RegisterCoreDependencies(this DependencyInjectionContainer container)
        {
            container.Add(block => block.Export<WebDataClient>().As<IDataClient>().UsingLifestyle(new SingletonLifestyle()));
            container.Add(block => block.Export<OpenWeatherService>().As<IWeatherService>());
            return container;
        }
    }
}