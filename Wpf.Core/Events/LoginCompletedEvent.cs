using Prism.Events;
using Wpf.App.Share.Models;

namespace Wpf.Core.Events
{
    /// <summary>
    /// 登录完成事件
    /// </summary>
    public class LoginCompletedEvent : PubSubEvent<CurrentUser>
    {
    }
}
