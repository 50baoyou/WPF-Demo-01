using Prism.Events;
using Wpf.Core.Events;

namespace Wpf.Core.Extension
{
    /// <summary>
    /// 事件聚合器扩展
    /// </summary>
    public static class EventAggregatorExtensions
    {
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="aggregator">事件聚合器</param>
        /// <param name="action">委托方法</param>
        /// <param name="filterName">过滤模块</param>
        public static SubscriptionToken ResgiterMessager(this IEventAggregator aggregator, Action<MessageModel> action, string filterName)
        {
            return aggregator.GetEvent<MessageEvent>().Subscribe(action, ThreadOption.PublisherThread, true, (p) =>
            {
                return p.FilterModule.Equals(filterName);
            });
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="aggregator">事件聚合器</param>
        /// <param name="targetName">目标模块</param>
        /// <param name="messageContentr">消息内容</param>
        public static void SendMessager(this IEventAggregator aggregator, string moduleName, string messageContent)
        {
            aggregator.GetEvent<MessageEvent>().Publish(new MessageModel()
            {
                FilterModule = moduleName,
                Content = messageContent
            });
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="aggregator">事件聚合器</param>
        /// <param name="subscriptionToken">订阅类型</param>
        public static void UnsubscribeMessager(this IEventAggregator aggregator, SubscriptionToken subscriptionToken)
        {
            aggregator.GetEvent<MessageEvent>().Unsubscribe(subscriptionToken);
        }

    }
}
