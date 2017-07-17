// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Hasher.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/3/17 10:47:40</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Toolkit
{
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Hasher
    /// </summary>
    public class Hasher
    {
        /// <summary>
        /// 获得输入字符串的MD5哈希值
        /// </summary>
        /// <param name="input">
        /// input 
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.string
        /// </returns>
        public static string GetMd5Hash(string input)
        {
            if (input == null)
            {
                input = string.Empty;
            }

            var data = Encoding.UTF8.GetBytes(input.Trim().ToLowerInvariant());
            using (var md5 = new MD5CryptoServiceProvider())
            {
                data = md5.ComputeHash(data);
            }

            var ret = new StringBuilder();
            foreach (var b in data)
            {
                ret.Append(b.ToString("x2").ToLowerInvariant());
            }

            return ret.ToString();
        }
    }
}
