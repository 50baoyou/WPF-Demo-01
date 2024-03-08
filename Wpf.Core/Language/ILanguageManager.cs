using Wpf.Core.Enums;

namespace Wpf.Core.Language
{
    public interface ILanguageManager
    {
        /// <summary>
        /// 获取或设置与指定键关联的本地化字符串
        /// </summary>
        /// <param name="key">用于检索本地化字符串的唯一键。</param>
        /// <returns>与指定键对应的本地化字符串。如果键不存在，则返回空字符串。</returns>
        string this[string key] { get; }

        /// <summary>
        /// 当前语言
        /// </summary>
        LanguageType CurrentLanague { get; }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="languageType"></param>
        void Set(LanguageType languageType);
    }
}
