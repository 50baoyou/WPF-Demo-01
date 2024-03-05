using Prism.Ioc;
using Prism.Modularity;
using System.Reflection;
using Wpf.Core.Extension;

namespace Wpf.Core
{
    public class CoreModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // 将 Core 模块内所有标注了 [ExposedService] 的类进行初始化
            containerProvider.InitializeAssembly(Assembly.GetExecutingAssembly());
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 将 Core 模块内所有标注了 [ExposedService] 的类进行注册
            containerRegistry.RegisterAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
