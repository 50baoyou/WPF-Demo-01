using System.Windows;
using System.Windows.Controls;
using Wpf.App.Login.ViewModels;
using Wpf.Core.Helper;

namespace Wpf.App.Login.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        // 处理用户密码（转换为哈希值）
        private void LoginPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (
                DataContext is not null
                && DataContext is LoginViewModel loginViewModel
                && sender is PasswordBox passwordBox
                )
            {
                loginViewModel.ProcessPasswordHash(passwordBox.GetSecurePasswordHash());
            }
        }
    }
}
