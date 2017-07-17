// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAuthorizeAttributeHelper.cs" company="zjzx">
//   2016-11-14 16:59:21
// </copyright>
// <author>李天赐</author>
// <date>2015/8/19 13:06:29</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc.Authorization
{
    using System.Web.Mvc;

    /// <summary>
    /// AuthorizeAttributeHelper接口
    /// </summary>
    public interface IAuthorizeAttributeHelper
    {
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="context">
        /// HTTP上下文
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Authorize(ActionExecutingContext context);
    }
}
