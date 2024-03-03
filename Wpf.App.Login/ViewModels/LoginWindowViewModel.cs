using Prism.Commands;
using Prism.Mvvm;
using System;

namespace Wpf.App.Login.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {

        public event EventHandler LoginCompleted;

        private bool isAuthenticated;

        public DelegateCommand LoginCommand { get; set; }

        public LoginWindowViewModel()
        {
            isAuthenticated = false;
            LoginCommand = new(Login);
        }

        private void Login()
        {
            isAuthenticated = true;

            if (isAuthenticated)
            {
                OnLoginCompleted(EventArgs.Empty);
            }
        }

        protected virtual void OnLoginCompleted(EventArgs e)
        {
            LoginCompleted?.Invoke(this, e);
        }
    }
}
