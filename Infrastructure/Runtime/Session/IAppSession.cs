// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppSession.cs" company="zjzx">
//   2016-11-25 14:10:38
// </copyright>
// <author>李天赐</author>
// <date>2016/6/27 13:20:57</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Runtime.Session
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// </summary>
    public interface IAppSession
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        int? UserId { get; }
    }
}
