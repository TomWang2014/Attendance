// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppAuthorizationException.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   没有权限的异常
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Exceptions
{
    /// <summary>
    /// 没有权限的异常
    /// </summary>
    public class AppAuthorizationException : System.Exception
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="message">
        /// 异常消息
        /// </param>
        public AppAuthorizationException(string message)
            : base(message)
        {
        }
    }
}
