// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionFilterAttribute.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/4/3 17:56:11</date>
// <summary>
//   主要功能有：
//   ExceptionFilterAttribute
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc.Filter
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    using Attendance.Infrastructure.Exceptions;
    using Infrastructure;

    /// <summary>
    /// ExceptionFilterAttribute, 自定义的异常过滤器
    /// </summary>
    public class ExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        /// <summary>
        /// 当出现异常时，需要跳转到的视图名称
        /// </summary>
        public string View { get; set; }

        /// <summary>
        /// 当发生异常时
        /// </summary>
        /// <param name="context">
        /// 当前异常上下文
        /// </param>
        public void OnException(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // Log.WriteLine(context.Exception);

            // If exception handled before, do nothing.
            // If this is child action, exception should be handled by main action.
            if (context.ExceptionHandled || context.IsChildAction)
            {
                return;
            }

            // TODO: Always log exception
            // LogHelper.LogException(context.Exception);

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.

            // if (!context.HttpContext.IsCustomErrorEnabled)
            // {
            // return;
            // }
            // Log.WriteLine("GetHttpCode" + new HttpException(null, context.Exception).GetHttpCode());

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, context.Exception).GetHttpCode() != 500)
            {
                return;
            }

            context.ExceptionHandled = true;

            // Log.WriteLine(context.Exception.Message);
            if (context.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                this.HandleAjaxException(context);
            }
            else
            {
                // Log.WriteLine(context.Exception.StackTrace);
                // 非ajax请求时发生的错误
                context.Result = this.HandleNonAjaxException(context);
            }
        }

        /// <summary>
        /// 处理AJAX异常
        /// </summary>
        /// <param name="context">
        /// 当前异常上下文
        /// </param>
        private void HandleAjaxException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;

            // Log.WriteLine("HandleAjaxException");
            if (context.Exception is UserFriendlyException)
            {
                // Log.WriteLine("Is UserFriendlyException");
                var userFriendlyException = context.Exception as UserFriendlyException;
                context.Result = new JsonResult
                {
                    Data = new
                    {
                        errorMessage = userFriendlyException.Message
                    }, 
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else if (context.Exception is ValidationException)
            {
                Infrastructure.Log.WriteLine(context.Exception);
                var validationException = context.Exception as ValidationException;
                var sb = new StringBuilder();
                foreach (var ex in validationException.ValidationErrors)
                {
                    sb.AppendLine();
                    sb.Append(ex.MemberNames);
                    sb.Append(" : ");
                    sb.Append(ex.ErrorMessage);
                    sb.AppendLine();
                }

                context.Result = new JsonResult
                {
                    Data = new
                    {
                        errorMessage = validationException.Message + "\n" + sb
                    }, 
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                Infrastructure.Log.WriteLine(context.Exception);
                context.Result = new JsonResult
                {
                    Data = new
                    {
                        errorMessage = context.Exception.Message
                    }, 
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                // #if (!DEBUG)
                // context.Result = new JsonResult
                // {
                // Data = new
                // {
                // errorMessage = "系统内部错误！"
                // },
                // JsonRequestBehavior = JsonRequestBehavior.AllowGet
                // };
                // #endif
            }
        }

        /// <summary>
        /// 处理非AJAX异常
        /// </summary>
        /// <param name="context">
        /// 当前异常上下文
        /// </param>
        /// <returns>
        /// AcitonResult
        /// </returns>
        private ActionResult HandleNonAjaxException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = 500;

            Infrastructure.Log.WriteLine(context.Exception);

            if (context.Exception is AppAuthorizationException)
            {
                return new ViewResult
                {
                    ViewName = "Unauthorized", 
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(new HandleErrorInfo(context.Exception, context.RouteData.Values["controller"].ToString(), context.RouteData.Values["action"].ToString()))
                };
            }


            return new ViewResult
            {
                ViewName = this.View, 
                ViewData = new ViewDataDictionary<HandleErrorInfo>(new HandleErrorInfo(context.Exception, context.RouteData.Values["controller"].ToString(), context.RouteData.Values["action"].ToString()))
            };
        }
    }

}
