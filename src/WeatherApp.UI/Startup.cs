using System;
using System.Collections.Generic;
using System.Text;
using Grace.DependencyInjection;
using WeatherApp.UI.ViewModels.Main;
using WeatherApp.UI.ViewModels.Main.Implementation;

namespace WeatherApp.UI
{
    public static class Startup
    {
        public static DependencyInjectionContainer RegisterUIDependencies(this DependencyInjectionContainer container)
        {
            container.Add(block => block.Export<MainViewModel>().As<IMainViewModel>());
            return container;
        }
    }
}
