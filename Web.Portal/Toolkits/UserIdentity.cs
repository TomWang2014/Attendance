// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserIdentity.cs" company="zjzx">
//   ©2016 中教在线 版权所有
// </copyright>
// <summary>
//   Defines the HomeController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Web.Portal.Toolkits
{
    using System.Web;

    using Attendance.Application.SystemMgtServices.Dtos;

    /// <summary>
    /// The user identity.
    /// </summary>
    public class UserIdentity
    {
        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        public static CurrentUserDto CurrentUser
        {
            get
            {
                var user = HttpContext.Current.Session["CurrentUserDto"];
                return user as CurrentUserDto;
            }

            set
            {
                HttpContext.Current.Session["CurrentUserDto"] = value;
            }
        }
    }
}