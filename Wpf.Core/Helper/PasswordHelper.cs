using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Controls;

namespace Wpf.Core.Helper
{
    /// <summary>
    /// 处理密码，将 SecurePassword 转换为安全的表示形式
    /// </summary>
    public static class PasswordHelper
    {
        /// <summary>
        /// 获取安全密码的哈希值。
        /// </summary>
        /// <param name="passwordBox">密码框实例。</param>
        /// <returns>返回安全密码的哈希值，如果密码为空或为null则返回null。</returns>
        public static string? GetSecurePasswordHash(this PasswordBox passwordBox)
        {
            if (passwordBox.SecurePassword == null)
            {
                return null;
            }

            var plainText = GetSecurePasswordAsString(passwordBox.SecurePassword);
            if (string.IsNullOrEmpty(plainText))
            {
                return null;
            }

            using var sha256 = SHA256.Create();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var hash = sha256.ComputeHash(plainTextBytes);

            return BitConverter.ToString(hash);
        }

        /// <summary>
        /// 将 SecureString 转换为字符串。
        /// </summary>
        /// <param name="securePassword">SecureString实例。</param>
        /// <returns>返回SecureString对应的字符串，如果SecureString为null则返回null。</returns>
        private static string? GetSecurePasswordAsString(SecureString securePassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
