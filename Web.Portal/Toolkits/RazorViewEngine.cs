//------------------------------------------------------------
// <copyright file="RazorViewEngine.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/12/7 13:37:03</date>
// <summary>
//  主要功能有：
//  
// </summary>
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attendance.Web.Portal.Toolkits
{
    using System.Web.Mvc;

    public class RazorViewEngine : BuildManagerViewEngine
    {
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            throw new NotImplementedException();
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var view = new RazorView(controllerContext, viewPath, layoutPath: masterPath, runViewStartPages: true, viewStartFileExtensions: FileExtensions, viewPageActivator: ViewPageActivator)
            {
            };

            return view;
        }
    }
}