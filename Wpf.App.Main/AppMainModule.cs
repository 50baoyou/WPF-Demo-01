using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wpf.App.Main.Views;
using Wpf.App.Share.Prism;

namespace Wpf.App.Main
{
    public class AppMainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
            regionManager.RegisterViewWithRegion<MainView>(RegionNames.MainRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
