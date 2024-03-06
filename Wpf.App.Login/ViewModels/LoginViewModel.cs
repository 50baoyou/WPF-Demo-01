using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Windows;
using Wpf.App.Share.Prism;
using Wpf.Core.Extension;

namespace Wpf.App.Login.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        public event EventHandler LoginCompleted;

        private bool _isAuthentication;

        private readonly IEventAggregator _eventAggregator;

        public static string Title => "WPF上位机综合应用";

        public DelegateCommand LoginCommand { get; set; }

        public DelegateCommand ExitAppCommand { get; set; }


        public LoginViewModel(IEventAggregator aggregator)
        {
            _eventAggregator = aggregator;
            _isAuthentication = false;
            LoginCommand = new(AuthenticateUser);
            ExitAppCommand = new(ExitApp);
        }

        private void AuthenticateUser()
        {
            _isAuthentication = true;

            if (_isAuthentication)
            {
                //LoginCompleted?.Invoke(this, EventArgs.Empty);
                _eventAggregator.SendMessager(ModuleNames.AppLoginModule, LoginStatus.LoginSuccessful);
            }
        }

        private void ExitApp()
        {
            Application.Current.Shutdown();
        }
    }
}
