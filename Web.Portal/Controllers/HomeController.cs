// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="zjzx">
//   ©2016 中教在线 版权所有
// </copyright>
// <summary>
//   Defines the HomeController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Web.Portal.Controllers
{
    using System.Web.Mvc;

    using Attendance.Application.SystemMgtServices;
    using Attendance.Web.Portal.Toolkits;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : AppAuthorizeController
    {
        /// <summary>
        /// 系统服务
        /// </summary>
        private readonly SystemServices systemServices;

        /// <summary>
        /// 够着函数
        /// </summary>
        /// <param name="systemServices">系统服务</param>
        public HomeController(SystemServices systemServices)
        {
            this.systemServices = systemServices;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns>系统首页</returns>
        public ActionResult Index()
        {
            ToolkitsHelper.ClientRouteInit(HttpContext, RouteData);
            ViewBag.userName = UserIdentity.CurrentUser.Name;
            ViewBag.roleName = UserIdentity.CurrentUser.ToString();
            return View(UserIdentity.CurrentUser.FuncItems);
        }

        /// <summary>
        /// 管理员首页面板展示
        /// </summary>
        /// <returns>view</returns>
        public ActionResult MainPanel()
        {
            return this.View();
        }

        /// <summary>
        /// 租户首页面板展示
        /// </summary>
        /// <returns></returns>
        public ActionResult TenantMainPanel()
        {
            return View();
        }

    }
}