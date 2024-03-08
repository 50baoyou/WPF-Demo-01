namespace Wpf.Core.Helper
{
    /// <summary>
    /// 断言工具类，判断参数或对象是否为空
    /// </summary>
    public static class AssertiontHelper
    {
        public static void NotNull<T>(T value, string paramName) where T : class
        {
            if (value is null)
            {
                throw new ArgumentNullException(paramName, $"The parameter '{paramName}' cannot be null.");
            }
        }

        public static void NotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"The argument '{paramName}' cannot be null or an empty string.", paramName);
            }
        }

        public static void NotNullOrDefault<T>(T? value, string paramName) where T : struct
        {
            if (value is null || value.Equals(default(T)))
            {
                throw new ArgumentException($"The argument '{paramName}' cannot be null or the default value for its type.", paramName);
            }
        }

        public static void CollectionNotEmpty<T>(ICollection<T> collection, string paramName)
        {
            if (collection is null || collection.Count is 0)
            {
                throw new ArgumentException($"The collection '{paramName}' cannot be null or empty.", paramName);
            }
        }
    }
}
