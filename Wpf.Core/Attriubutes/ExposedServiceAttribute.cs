namespace Wpf.Core.Ioc
{
    /// <summary>
    /// 类型的生命周期枚举
    /// </summary>
    public enum LifeTime
    {
        // 单例，无论何时请求该服务，DI容器都将返回同一实例
        Singleton,
        // 多例，每次请求该服务时，DI容器都会创建并返回一个新的实例
        Transient
    }

    /// <summary>
    /// 标注类型的生命周期是否自动初始化
    /// AttributeTargets.Class 自定义特性的对象
    /// AllowMultiple: 是否允许被多次使用
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExposedServiceAttribute : Attribute
    {
        public LifeTime LifeTime { get; set; }

        public bool AutoInitialize { get; set; }

        public Type[] Types { get; set; }

        public ExposedServiceAttribute(LifeTime lifeTime = LifeTime.Transient, params Type[] types)
        {
            LifeTime = lifeTime;
            Types = types;
        }
    }
}
