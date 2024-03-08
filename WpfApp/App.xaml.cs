using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using Wpf.App.Login;
using Wpf.App.Main;
using Wpf.App.Share.Models;
using Wpf.App.Share.Prism;
using Wpf.Core;
using Wpf.Core.Events;
using Wpf.Core.Language;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<LoginWindow>();
        }

        // 注册服务
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILanguageManager, LanguageManager>();
        }

        // 注册模块
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            // 应用层模块
            moduleCatalog.AddModule<AppLoginModule>();
            moduleCatalog.AddModule(new ModuleInfo()
            {
                // 配置 AppMainModule 模块为按需加载
                ModuleName = typeof(AppMainModule).Name,
                ModuleType = typeof(AppMainModule).AssemblyQualifiedName,
                InitializationMode = InitializationMode.OnDemand
            });

            // 核心层模块
            moduleCatalog.AddModule<CoreModule>();
        }

        // 初始化完成后调用
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 订阅登录模块事件
            var eventAggregator = Container.Resolve<IEventAggregator>();
            eventAggregator.GetEvent<LoginCompletedEvent>().Subscribe(OnLoginCompleted, ThreadOption.UIThread);
        }

        /// <summary>
        /// 登录完成后打开主界面
        /// </summary>
        /// <param name="currentUser"></param>
        private void OnLoginCompleted(CurrentUser currentUser)
        {
            if (currentUser is not null)
            {
                // 加载应用程序主模块
                var moduleManager = Container.Resolve<IModuleManager>();
                moduleManager.LoadModule(ModuleNames.AppMainModule);

                // 创建主界面窗口
                var mainWindow = Container.Resolve<MainWindow>();
                mainWindow.Show();

                // 关闭登录窗口
                Current.MainWindow.Close();
            }
        }
    }
}
