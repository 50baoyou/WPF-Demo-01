using Prism.Events;

namespace Wpf.Core.Events
{
    /// <summary>
    /// 消息事件
    /// </summary>
    public class MessageEvent : PubSubEvent<MessageModel>
    {
    }


    public class MessageModel
    {
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; } = null!;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = null!;
    }
}
