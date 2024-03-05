using Prism.Ioc;
using System.Reflection;
using Wpf.Core.Ioc;

namespace Wpf.Core.Extension
{
    /// <summary>
    /// 依赖注入扩展（实现加载模块时，实例化标注为 ExposedServiceAttriubute 特性的类）
    /// </summary>
    public static class DependencyExtension
    {
        /// <summary>
        /// 从指定的<see cref="Assembly"/>中获取所有标记有<see cref="ExposedServiceAttribute"/>的<see cref="Type"/>。
        /// </summary>
        /// <param name="assembly">要搜索的程序集。</param>
        /// <returns>包含所有标记有<see cref="ExposedServiceAttribute"/>的<see cref="Type"/>的列表。</returns>
        private static List<Type> GetTypes(Assembly assembly)
        {
            var result = assembly.GetTypes()
                .Where(t => t != null && t.IsClass && !t.IsAbstract
                && t.CustomAttributes.Any(p => p.AttributeType == typeof(ExposedServiceAttribute)))
                .ToList();

            return result;
        }

        /// <summary>
        /// 注册指定<see cref="Assembly"/>中所有具有<see cref="ExposedServiceAttribute"/>特性的类型。
        /// </summary>
        /// <param name="container">用于注册类型的容器。</param>
        /// <param name="assembly">要搜索的程序集。</param>
        public static void RegisterAssembly(this IContainerRegistry container, Assembly assembly)
        {
            var list = GetTypes(assembly);

            foreach (var type in list)
            {
                RegisterAssembly(container, type);
            }
        }

        /// <summary>
        /// 获取指定<see cref="Type"/>上所有的<see cref="ExposedServiceAttribute"/>。
        /// </summary>
        /// <param name="type">要搜索的类型。</param>
        /// <returns>指定<see cref="Type"/>上所有的<see cref="ExposedServiceAttribute"/>的集合。</returns>
        private static IEnumerable<ExposedServiceAttribute> GetExposedServices(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.GetCustomAttributes<ExposedServiceAttribute>();
        }

        /// <summary>
        /// 注册具有<see cref="ExposedServiceAttribute"/>特性的指定<see cref="Type"/>。
        /// </summary>
        /// <param name="container">用于注册类型的容器。</param>
        /// <param name="type">要注册的类型。</param>
        public static void RegisterAssembly(IContainerRegistry container, Type type)
        {
            var list = GetExposedServices(type).ToList();

            foreach (var item in list)
            {
                if (item.LifeTime == LifeTime.Singleton)
                {
                    container.RegisterSingleton(type); // 注册单例
                }

                foreach (var IType in item.Types)
                {
                    //if (item.LifeTime == LifeTime.Singleton)
                    //{
                    //    container.RegisterSingleton(IType, type); 
                    //}
                    //else if (item.LifeTime == LifeTime.Transient)
                    //{
                    //    container.Register(IType, type); // 以接口注册多例
                    //}

                    // 服务的实际类型也被注册为单例
                    container.Register(IType, type); // 以接口注册多例
                }
            }
        }

        /// <summary>
        /// 初始化指定<see cref="Assembly"/>中所有具有<see cref="ExposedServiceAttribute"/>特性的类型。
        /// </summary>
        /// <param name="container">用于初始化类型的容器。</param>
        /// <param name="assembly">要搜索的程序集。</param>
        public static void InitializeAssembly(this IContainerProvider container, Assembly assembly)
        {
            var list = GetTypes(assembly);

            foreach (var item in list)
            {
                InitializeAssembly(container, item);
            }
        }

        /// <summary>
        /// 初始化具有<see cref="ExposedServiceAttribute"/>特性的指定<see cref="Type"/>。
        /// </summary>
        /// <param name="container">用于初始化类型的容器。</param>
        /// <param name="type">要初始化的类型。</param>
        private static void InitializeAssembly(IContainerProvider container, Type type)
        {
            var list = GetExposedServices(type);

            foreach (var item in list)
            {
                if (item.LifeTime == LifeTime.Singleton && item.AutoInitialize)
                {
                    container.Resolve(type);
                }
            }
        }
    }
}
