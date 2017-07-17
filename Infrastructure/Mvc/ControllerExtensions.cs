// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ControllerExtensions.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/5/7 10:07:29</date>
// <summary>
//   主要功能有：
//   控制器扩展类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web.Mvc;

    using Attendance.Infrastructure.Collections.Extensions;
    using Attendance.Infrastructure.Dto;
    using Attendance.Infrastructure.Mvc.Jsonp;

    /// <summary>
    /// ControllerExtensions
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Extension methods for the controller to allow jsonp.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// JsonpResult
        /// </returns>
        public static JsonpResult Jsonp(this Controller controller, object data)
        {
            var result = new JsonpResult()
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

            return result;
        }
    }
}
