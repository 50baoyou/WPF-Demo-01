using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Services.Dialogs;
using Wpf.Core.Ioc;

namespace Wpf.Core.Helper
{
    [ExposedService(LifeTime.Singleton, AutoInitialize = true)]
    public class PrismProviderHelper
    {
        /// <summary>
        /// DI容器
        /// </summary>
        public static IContainerExtension Container { get; private set; } = null!;

        /// <summary>
        /// 区域管理器
        /// </summary>
        public static IRegionManager RegionManager { get; private set; } = null!;

        /// <summary>
        /// 对话框管理器
        /// </summary>
        public static IDialogService DialogService { get; private set; } = null!;

        /// <summary>
        /// 事件聚合器
        /// </summary>
        public static IEventAggregator EventAggregator { get; private set; } = null!;

        /// <summary>
        /// 模块管理器
        /// </summary>
        public static IModuleManager ModuleManager { get; private set; } = null!;

        public PrismProviderHelper(
            IContainerExtension container,
            IRegionManager regionManager,
            IDialogService dialogService,
            IEventAggregator eventAggregator,
            IModuleManager moduleManager
            )
        {
            Container = container;
            RegionManager = regionManager;
            DialogService = dialogService;
            EventAggregator = eventAggregator;
            ModuleManager = moduleManager;
        }
    }
}
