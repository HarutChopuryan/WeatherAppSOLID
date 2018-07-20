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
            container.Add(block => block.Export<ItemsViewModel>().As<IItemsViewModel>());
            return container;
        }
    }
}