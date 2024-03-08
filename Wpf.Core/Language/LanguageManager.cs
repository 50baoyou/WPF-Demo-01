using System.Windows;
using System.Windows.Markup;
using Wpf.Core.Dialogs;
using Wpf.Core.Enums;

namespace Wpf.Core.Language
{
    public class LanguageManager : ILanguageManager
    {
        private ResourceDictionary? _currentLanguageDictionary;

        private const string _uri = "pack://application:,,,/Wpf.UI;component/Languages";

        // 索引器
        public string this[string key]
        {
            get
            {
                if (_currentLanguageDictionary is not null && _currentLanguageDictionary.Contains(key))
                {
                    return _currentLanguageDictionary[key].ToString() ?? string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public LanguageType CurrentLanague { get; private set; }

        public LanguageManager()
        {
            Set(LanguageType.SimplifiedChinese); // 设置默认语言
        }

        public void Set(LanguageType languageType)
        {
            LoadLanguageDictionary(languageType.ToString());

            if (CurrentLanague != languageType)
            {
                CurrentLanague = languageType;

                // TODO：保存到系统设置文件中
            }
        }

        private void LoadLanguageDictionary(string languageName)
        {
            if (_currentLanguageDictionary is not null)
            {
                // 将资源字典从当前应用程序删除
                Application.Current.Resources.MergedDictionaries.Remove(_currentLanguageDictionary);
            }

            var dictionaryUri = new Uri($"{_uri}/{languageName}Language.xaml", UriKind.Absolute);
            var streamInfo = Application.GetResourceStream(dictionaryUri);
            if (streamInfo is null || streamInfo.Stream is null)
            {
                PopupBox.Show("解析语言文件失败，请重新启动应用或联系管理员。");
                return;
            }

            if (XamlReader.Load(streamInfo.Stream) is ResourceDictionary languageDictionary)
            {
                _currentLanguageDictionary = languageDictionary;
                // 将资源字典应用到当前应用程序
                Application.Current.Resources.MergedDictionaries.Add(_currentLanguageDictionary);
            }
            else
            {
                PopupBox.Show("加载语言文件失败，请重新启动应用或联系管理员。");
            }
        }
    }
}
