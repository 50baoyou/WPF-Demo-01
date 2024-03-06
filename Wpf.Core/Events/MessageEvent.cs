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
        /// 目标模块
        /// </summary>
        public string FilterModule { get; set; } = null!;

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; } = null!;
    }
}
