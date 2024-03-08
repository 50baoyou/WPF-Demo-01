using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Windows;
using Wpf.App.Share.Models;
using Wpf.Core.Dialogs;
using Wpf.Core.Enums;
using Wpf.Core.Events;
using Wpf.Core.Language;

namespace Wpf.App.Login.ViewModels
{
    public class LoginViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;

        private readonly ILanguageManager _languageManager;

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string Password { get; set; }

        public DelegateCommand LoginCommand { get; set; }

        public DelegateCommand ExitAppCommand { get; set; }

        public DelegateCommand<string> SwitchLanguageCommand { get; set; }


        public LoginViewModel(IEventAggregator aggregator, ILanguageManager languageManager)
        {
            _eventAggregator = aggregator;
            _languageManager = languageManager;
            LoginCommand = new(AuthenticateUser);
            ExitAppCommand = new(ExitApp);
            SwitchLanguageCommand = new(SwitchLanguage);
        }

        /// <summary>
        /// 获取密码哈希并转换为字符串
        /// </summary>
        /// <param name="passwordHash">密码哈希</param>
        public void ProcessPasswordHash(string passwordHash)
        {
            if (!string.IsNullOrEmpty(passwordHash))
            {
                Password = passwordHash;
            }
        }

        /// <summary>
        /// 验证登录信息
        /// </summary>
        private void AuthenticateUser()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                PopupBox.Show("用户名或密码不能为空。");
                return;
            }

            var currentUserName = UserName.Trim();
            var currentPassword = Password.Trim();
            if (currentUserName == "admin" && currentPassword.Length > 0)
            {
                // 创建用户配置
                var userProfile = new CurrentUser()
                {
                    UsertName = currentUserName,
                    Password = currentPassword
                };
                // 发布事件
                _eventAggregator.GetEvent<LoginCompletedEvent>().Publish(userProfile);
            }
            else
            {
                PopupBox.Show("用户名或密码错误。");
            }
        }

        /// <summary>
        /// 退出应用程序
        /// </summary>
        private void ExitApp()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="language">目标语言</param>
        private void SwitchLanguage(string language)
        {
            if (Enum.TryParse(language, true, out LanguageType languageType))
            {
                switch (languageType)
                {
                    case LanguageType.SimplifiedChinese:
                        _languageManager.Set(LanguageType.SimplifiedChinese);
                        break;
                    case LanguageType.English:
                        _languageManager.Set(LanguageType.English);
                        break;
                    default:
                        PopupBox.Show("语言切换失败。");
                        break;
                }
            }
            else
            {
                PopupBox.Show("语言参数无效。");
            }
        }
    }
}
