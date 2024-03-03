using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Wpf.App.Login.ViewModels;
using Wpf.App.Login.Views;
using Wpf.App.Main.Views;

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
            // 注册视图
            containerRegistry.RegisterSingleton<LoginWindow>();
            containerRegistry.RegisterSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var loginWindow = Container.Resolve<LoginWindow>();
            if (loginWindow.DataContext is LoginWindowViewModel loginWindowViewModel)
            {
                loginWindowViewModel.LoginCompleted += OnLoginCompleted;
            }
        }

        private void OnLoginCompleted(object? sender, EventArgs e)
        {
            var minWindow = Container.Resolve<MainWindow>();
            minWindow.Show();

            var loginWindow = Container.Resolve<LoginWindow>();
            loginWindow.Close();
        }
    }
}
