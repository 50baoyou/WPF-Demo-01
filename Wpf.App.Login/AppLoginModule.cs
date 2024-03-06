using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Wpf.App.Login.Views;
using Wpf.App.Share.Prism;

namespace Wpf.App.Login
{
    public class AppLoginModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<RegionManager>();
            regionManager.RegisterViewWithRegion<LoginView>(RegionNames.LoginRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
