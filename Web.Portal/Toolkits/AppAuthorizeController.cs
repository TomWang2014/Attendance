// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppAuthorizeController.cs" company="zjzx">
//   ©2016 中教在线 版权所有
// </copyright>
// <summary>
//   Defines the HomeController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Web.Portal.Toolkits
{
    using System.Web.Mvc;

    using Attendance.Infrastructure.Mvc.Filter;

    /// <summary>
    /// 需要权限的控制器基类
    /// </summary>
    [Exception(View = "Error")]
    [AppAuthorize]
    public class AppAuthorizeController : Controller
    {  
    }
}