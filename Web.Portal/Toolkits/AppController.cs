// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppController.cs" company="zjzx">
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
    /// 非权限控制器基类
    /// </summary>
    [Exception(View = "Error")]
    public class AppController : Controller
    {
    }
}