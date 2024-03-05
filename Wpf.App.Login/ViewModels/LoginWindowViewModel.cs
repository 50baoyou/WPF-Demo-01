using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Wpf.App.Login.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        public event EventHandler LoginCompleted;

        private bool _isAuthentication;

        public DelegateCommand LoginCommand { get; set; }
        public LoginWindowViewModel()
        {
            _isAuthentication = false;
            LoginCommand = new(AuthenticateUser);
        }

        private void AuthenticateUser()
        {
            _isAuthentication = true;

            if (_isAuthentication)
            {
                LoginCompleted?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
