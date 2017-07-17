// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ToolkitsHelper.cs" company="zjzx">
//   2016-11-19 13:21:06
// </copyright>
// <author>李天赐</author>
// <date>2016/7/7 16:16:01</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Web.Portal.Toolkits
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Xml;

    using Attendance.Infrastructure;
    using Attendance.Infrastructure.Cache;
    using Attendance.Infrastructure.Collections.Extensions;
    using Attendance.Infrastructure.Dto;
    using Attendance.Infrastructure.Exceptions;
    using Attendance.Infrastructure.Toolkit;
    using Attendance.Infrastructure.Unity.Ioc;
    using Attendance.Web.Portal.Models;

    /// <summary>
    /// 学生端mvc扩展类
    /// </summary>
    public static class ToolkitsHelper
    {
        /// <summary>
        /// 生成包含url的js文件
        /// </summary>
        /// <param name="httpContext">请求上下文</param>
        /// <param name="routeData">路由集合</param>
        public static void ClientRouteInit(HttpContextBase httpContext, RouteData routeData)
        {
            var cache = IocManager.Instance.Resolve<ICache>();

            var list = cache.Get<List<EntityActionsDto>>("paths") ?? ClientRoutes(httpContext, routeData);

            var str = JsonHelper.Obj2JsonStr(list);
            var savePath = httpContext.Server.MapPath("~/Scripts/static/paths.js");
            var content = string.Format("{0}{1}", "var allPaths =", str);
            File.WriteAllText(savePath, content);
        }

        /// <summary>
        /// 获得当期系统所有的action集合
        /// </summary>
        /// <param name="httpContext"> The http Context. </param>
        /// <param name="routeData"> The route Data. </param>
        /// <returns> 返回path列表 </returns>
        public static List<EntityActionsDto> ClientRoutes(HttpContextBase httpContext, RouteData routeData)
        {
            var list = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var cache = IocManager.Instance.Resolve<ICache>();

            var acionts = new List<EntityActionsDto>();
            foreach (var assembly in list.Distinct())
            {
                try
                {
                    Type[] typesInThisAssembly;

                    try
                    {
                        typesInThisAssembly = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException ex)
                    {
                        typesInThisAssembly = ex.Types;
                    }

                    if (typesInThisAssembly.IsNullOrEmpty())
                    {
                        continue;
                    }

                    foreach (var type in typesInThisAssembly)
                    {
                        if (!type.IsSubclassOf(typeof(AppAuthorizeController)) && !type.IsSubclassOf(typeof(AppController)))
                        {
                            continue;
                        }

                        var areaName = string.Empty;
                        var areaAttribute = type.GetCustomAttribute<AreasAttribute>();
                        if (areaAttribute != null)
                        {
                            areaName = areaAttribute.AreaName.Trim();
                        }

                        var methods = type.FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly, Type.FilterName, "*");

                        foreach (var memberInfo in methods)
                        {
                            if (memberInfo.DeclaringType == null)
                            {
                                continue;
                            }

                            var model = new EntityActionsDto
                            {
                                Action = memberInfo.Name.ToLower().Trim(),
                                Area = areaName.ToLower(),
                                Controller = memberInfo.DeclaringType.Name.Replace("Controller", string.Empty).ToLower(),
                                Url = GetUrlByParameter(httpContext, routeData, areaName.ToLower().Trim(), memberInfo.DeclaringType.Name.Replace("Controller", string.Empty).ToLower(), memberInfo.Name.ToLower())
                            };

                            acionts.Add(model);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.WriteLine(ex);
                }
            }

            cache.Add("paths", acionts);
            return acionts;
        }

        /// <summary>
        /// 根据参数获得当前上下午参数集合
        /// </summary>
        /// <param name="httpContext">httpcontext</param>
        /// <param name="routeData">路由集合</param>
        /// <param name="areaName">area</param>
        /// <param name="controllerName">控制器</param>
        /// <param name="actionName">action</param>
        /// <returns>返回生成的url</returns>
        public static string GetUrlByParameter(HttpContextBase httpContext, RouteData routeData, string areaName, string controllerName, string actionName)
        {
            var url = new UrlHelper(new RequestContext(httpContext, routeData));
            return url.Action(actionName, controllerName, new { area = areaName });
        }

        /// <summary>
        /// 返回配置文件中的权限列表
        /// </summary>
        /// <returns>权限集合</returns>
        public static List<AuthorityUrls> InitAllFunc()
        {
            try
            {
                var cache = IocManager.Instance.Resolve<ICache>();

                var all = cache.Get<List<AuthorityUrls>>("AllFunc");
                if (all != null)
                {
                    return all;
                }

                var list = new List<AuthorityUrls>();
                var doc = new XmlDocument();
                doc.Load(File.OpenText(HttpContext.Current.Server.MapPath("/config/authority.config")));

                var groupList = doc.GetElementsByTagName("container");

                foreach (XmlNode item in groupList)
                {
                    if (item.Attributes != null)
                    {
                        var needAreaName = item.Attributes["areaName"].Value;
                        var needControllerName = item.Attributes["controllerName"].Value;
                        var needActionName = item.Attributes["actionName"].Value;

                        // 组装url
                        var roleName = StringUrlExtension.GetRequestUrlByParameter(
                            needAreaName,
                            needControllerName,
                            needActionName);

                        var model = new AuthorityUrls { RoleName = roleName };

                        foreach (XmlNode chin in item.ChildNodes)
                        {
                            if (chin.Attributes == null || chin.Attributes["controllerName"] == null || chin.Attributes["actionName"] == null)
                            {
                                continue;
                            }

                            var controllerName = chin.Attributes["controllerName"].Value;
                            var actionName = chin.Attributes["actionName"].Value;

                            var includerul = StringUrlExtension.GetRequestUrlByParameter(needAreaName, controllerName, actionName);

                            model.IncludeUrl.Add(includerul);
                        }

                        list.Add(model);
                    }
                }

                cache.Add("AllFunc", list, 60 * 60 * 12);
                return list;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("读取权限列表失败！" + ex);
            }
        }
    }
}