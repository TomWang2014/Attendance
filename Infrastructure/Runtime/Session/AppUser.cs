//------------------------------------------------------------
// <copyright file="AppUser.cs" company="zjzx">
//    ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/11/25 14:13:47</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------

namespace Attendance.Infrastructure.Runtime.Session
{
    using Attendance.Infrastructure.Exceptions;
    using Attendance.Infrastructure.Unity.Ioc;

    /// <summary>
    /// AppUser
    /// </summary>
    public class AppUser
    {
        /// <summary>
        /// 当前登录用户id
        /// </summary>
        public static int Id
        {
            get
            {
                var appSession = IocManager.Instance.Resolve<IAppSession>();
                if (appSession.UserId == null)
                {
                    throw new UserFriendlyException("抱歉，您的登录已经失效，请刷新页面重试！");
                }

                return appSession.UserId.Value;
            }
        }
    }
}
