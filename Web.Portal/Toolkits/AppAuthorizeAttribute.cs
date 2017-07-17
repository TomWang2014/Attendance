// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppAuthorizeAttribute.cs" company="zjzx">
//   ©2016 中教在线 版权所有
// </copyright>
// <summary>
//   Defines the HomeController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Web.Portal.Toolkits
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using Attendance.Infrastructure.Exceptions;
    using Attendance.Infrastructure.Mvc.Authorization;
    using Attendance.Infrastructure.Toolkit;

    /// <summary>
    /// 需要权限的控制器基类
    /// </summary>
    public class AppAuthorizeAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行之前调用
        /// </summary>
        /// <param name="filterContext">
        /// 过滤器上下文
        /// </param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var isAjaxRequest = filterContext.HttpContext.Request.Headers["X-Requested-With"] != null && filterContext.HttpContext.Request.Headers["X-Requested-With"].ToLower() == "xmlhttprequest";
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var areaName = (filterContext.RouteData.DataTokens["area"] ?? string.Empty).ToString();

            var requestUrl = StringUrlExtension.GetRequestUrlByParameter(areaName, controllerName, actionName);

            // 没有经过登录验证
            if (UserIdentity.CurrentUser == null)
            {
                if (isAjaxRequest)
                {
                    filterContext.HttpContext.Response.StatusCode = 401;
                    filterContext.Result = new JsonResult
                     {
                         Data = new { errorMessage = "抱歉，您的登录已经失效，请刷新页面重试！" },
                         JsonRequestBehavior = JsonRequestBehavior.AllowGet
                     };
                    return;
                }

                // 同步请求，直接返回登录页面
                filterContext.Result = new RedirectResult("/account/Index");
                return;
            }

            // 默认开放首页权限
            if (controllerName.ToLower() == "home")
            {
                return;
            }


            // 不需要权限验证
            if (filterContext.ActionDescriptor.IsDefined(typeof(AnonymousAttribute), true))
            {
                return;
            }

            // 已经登录，判断是否有访问该Url的权限
            if (UserIdentity.CurrentUser.AuthenticationUrl.FirstOrDefault(a => a == requestUrl) == null)
            {
                if (!isAjaxRequest)
                {
                    throw new AppAuthorizationException("抱歉，您对" + requestUrl + "访问没有权限,请检查配置。");
                }

                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new { errorMessage = "抱歉，您对" + requestUrl + "访问没有权限,请检查配置。" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
    }
}