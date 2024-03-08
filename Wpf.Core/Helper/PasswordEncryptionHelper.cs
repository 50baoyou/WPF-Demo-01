namespace Wpf.Core.Helper
{
    /// <summary>
    /// 密码加密，使用 BCrypt.Net-Next
    /// </summary>
    public class PasswordEncryptionHelper
    {
        private static readonly int DefaultCost = 11; // 默认的计算成本

        /// <summary>
        /// 使用 BCrypt.Net.BCrypt 哈希密码。
        /// </summary>
        /// <param name="password">需要哈希的密码。</param>
        /// <returns>哈希后的密码。</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password.Trim(), DefaultCost);
        }

        /// <summary>
        /// 验证密码是否与哈希值匹配。
        /// </summary>
        /// <param name="passwordToCheck">需要验证的密码。</param>
        /// <param name="hashedPassword">已哈希的密码。</param>
        /// <returns>如果密码匹配，则为true；否则为false。</returns>
        public static bool Verify(string passwordToCheck, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(passwordToCheck.Trim(), hashedPassword.Trim());
        }
    }
}
