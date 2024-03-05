using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;
using Wpf.App.Login;
using Wpf.App.Login.ViewModels;
using Wpf.App.Login.Views;
using Wpf.App.Main;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private Window? _currentWindow;

        protected override Window CreateShell()
        {
            _currentWindow ??= Container.Resolve<LoginWindow>();
            // 订阅登录完成事件
            if (_currentWindow.DataContext is LoginWindowViewModel loginWindowViewModel)
            {
                loginWindowViewModel.LoginCompleted += OnLoginCompleted;
            }
            else
            {
                MessageBox.Show("程序异常，请联系管理员。");
            }

            return _currentWindow;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }

        // 注册模块
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            moduleCatalog.AddModule<AppLoginModule>();
            moduleCatalog.AddModule<AppMainModule>();
        }

        private void OnLoginCompleted(object? sender, EventArgs e)
        {
            // 创建主窗口
            var minWindow = Container.Resolve<MainWindow>();
            minWindow.Show();

            // 关闭登录窗口
            _currentWindow?.Close();
        }
    }
}
