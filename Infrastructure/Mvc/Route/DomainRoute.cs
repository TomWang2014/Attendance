// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainRoute.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2016/5/24 17:53:12</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc.Route
{
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// 路由配置扩展
    /// </summary>
    public class DomainRoute : Route
    {
        /// <summary>
        /// The domain regex.
        /// </summary>
        private Regex domainRegex;

        /// <summary>
        /// The path regex.
        /// </summary>
        private Regex pathRegex;

        /// <summary>
        /// Gets or sets the domain.
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRoute"/> class.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="defaults">
        /// The defaults.
        /// </param>
        public DomainRoute(string domain, string url, RouteValueDictionary defaults)
            : base(url, defaults, new MvcRouteHandler())
        {
            this.Domain = domain;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRoute"/> class.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="defaults">
        /// The defaults.
        /// </param>
        /// <param name="routeHandler">
        /// The route handler.
        /// </param>
        public DomainRoute(string domain, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
            : base(url, defaults, routeHandler)
        {
            this.Domain = domain;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRoute"/> class.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="defaults">
        /// The defaults.
        /// </param>
        public DomainRoute(string domain, string url, object defaults)
            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
        {
            this.Domain = domain;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainRoute"/> class.
        /// </summary>
        /// <param name="domain">
        /// The domain.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="defaults">
        /// The defaults.
        /// </param>
        /// <param name="routeHandler">
        /// The route handler.
        /// </param>
        public DomainRoute(string domain, string url, object defaults, IRouteHandler routeHandler)
            : base(url, new RouteValueDictionary(defaults), routeHandler)
        {
            this.Domain = domain;
        }

        /// <summary>
        /// The get route data.
        /// </summary>
        /// <param name="httpContext">
        /// The http context.
        /// </param>
        /// <returns>
        /// The <see cref="RouteData"/>.
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            // 构造 regex
            this.domainRegex = this.CreateRegex(this.Domain);
            this.pathRegex = this.CreateRegex(this.Url);

            // 请求信息
            var requestDomain = httpContext.Request.Headers["host"];
            if (!string.IsNullOrEmpty(requestDomain))
            {
                if (requestDomain.IndexOf(":", System.StringComparison.Ordinal) > 0)
                {
                    requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":", System.StringComparison.Ordinal));
                }
            }
            else
            {
                requestDomain = httpContext.Request.Url.Host;
            }

            var requestPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;

            // 匹配域名和路由
            var domainMatch = this.domainRegex.Match(requestDomain);
            var pathMatch = this.pathRegex.Match(requestPath);

            // 路由数据
            RouteData data = null;
            if (!domainMatch.Success || !pathMatch.Success)
            {
                return data;
            }

            data = new RouteData(this, this.RouteHandler);

            // 添加默认选项
            if (this.Defaults != null)
            {
                foreach (var item in this.Defaults)
                {
                    data.Values[item.Key] = item.Value;
                    if (item.Key.Equals("area") || item.Key.Equals("Namespaces"))
                    {
                        data.DataTokens[item.Key] = item.Value;
                    }
                }
            }

            // 匹配域名路由
            for (var i = 1; i < domainMatch.Groups.Count; i++)
            {
                var group = domainMatch.Groups[i];
                if (@group.Success)
                {
                    var key = this.domainRegex.GroupNameFromNumber(i);

                    if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
                    {
                        if (!string.IsNullOrEmpty(@group.Value))
                        {
                            data.Values[key] = @group.Value;
                            if (key.Equals("area"))
                            {
                                data.DataTokens[key] = @group.Value;
                            }
                        }
                    }
                }
            }

            // 匹配域名路径
            for (var i = 1; i < pathMatch.Groups.Count; i++)
            {
                var group = pathMatch.Groups[i];
                if (@group.Success)
                {
                    var key = this.pathRegex.GroupNameFromNumber(i);

                    if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
                    {
                        if (!string.IsNullOrEmpty(@group.Value))
                        {
                            data.Values[key] = @group.Value;
                            if (key.Equals("area"))
                            {
                                data.DataTokens[key] = @group.Value;
                            }
                        }
                    }
                }
            }

            return data;
        }

        /// <summary>
        /// The get virtual path.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The <see cref="VirtualPathData"/>.
        /// </returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return base.GetVirtualPath(requestContext, this.RemoveDomainTokens(values));
        }

        /// <summary>
        /// The get domain data.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The <see cref="DomainData"/>.
        /// </returns>
        public DomainData GetDomainData(RequestContext requestContext, RouteValueDictionary values)
        {
            // 获得主机名
            var hostname = this.Domain;
            foreach (var pair in values)
            {
                hostname = hostname.Replace("{" + pair.Key + "}", pair.Value.ToString());
            }

            // Return 域名数据
            return new DomainData
            {
                Protocol = "http", 
                HostName = hostname, 
                Fragment = string.Empty
            };
        }

        /// <summary>
        /// The create regex.
        /// </summary>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <returns>
        /// The <see cref="Regex"/>.
        /// </returns>
        private Regex CreateRegex(string source)
        {
            // 替换
            source = source.Replace("/", @"\/?");
            source = source.Replace(".", @"\.?");
            source = source.Replace("-", @"\-?");
            source = source.Replace("{", @"(?<");
            source = source.Replace("}", @">([a-zA-Z0-9_]*))");

            return new Regex("^" + source + "$");
        }

        /// <summary>
        /// The remove domain tokens.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The <see cref="RouteValueDictionary"/>.
        /// </returns>
        private RouteValueDictionary RemoveDomainTokens(RouteValueDictionary values)
        {
            var tokenRegex = new Regex(@"({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?");
            var tokenMatch = tokenRegex.Match(this.Domain);
            for (var i = 0; i < tokenMatch.Groups.Count; i++)
            {
                var group = tokenMatch.Groups[i];
                if (group.Success)
                {
                    var key = group.Value.Replace("{", string.Empty).Replace("}", string.Empty);
                    if (values.ContainsKey(key))
                        values.Remove(key);
                }
            }

            return values;
        }
    }
}