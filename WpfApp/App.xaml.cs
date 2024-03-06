using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using Wpf.App.Login;
using Wpf.App.Main;
using Wpf.App.Share.Prism;
using Wpf.Core;
using Wpf.Core.Events;
using Wpf.Core.Extension;
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

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

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
            eventAggregator.ResgiterMessager(OnLoginCompleted, ModuleNames.AppLoginModule);
        }

        private void OnLoginCompleted(MessageModel model)
        {
            if (model.Content is LoginStatus.LoginSuccessful)
            {
                // 加载应用程序主模块
                var moduleManager = Container.Resolve<IModuleManager>();
                moduleManager.LoadModule(ModuleNames.AppMainModule);
                //PrismProvider.ModuleManager.LoadModule(ModuleNames.AppMainModule);

                // 创建主界面窗口
                var mainWindow = Container.Resolve<MainWindow>();
                mainWindow.Show();

                // 关闭登录窗口
                Current.MainWindow.Close();
            }
        }
    }
}
