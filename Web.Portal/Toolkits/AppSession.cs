// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppSession.cs" company="zjzx">
//   ©2016 中教在线 版权所有
// </copyright>
// <summary>
//   Defines the HomeController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Web.Portal.Toolkits
{
    using Attendance.Infrastructure.Runtime.Session;

    /// <summary>
    ///  IAppSession的实现
    /// </summary>
    public class AppSession : IAppSession
    {
        /// <summary>
        /// 当前用户id
        /// </summary>
        public int? UserId
        {
            get
            {
                if (UserIdentity.CurrentUser != null)
                {
                    return UserIdentity.CurrentUser.Id;
                }

                return null;
            }
        }
    }
}