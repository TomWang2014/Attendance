// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonpResult.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/5/7 10:02:25</date>
// <summary>
//   主要功能有：
//   JsonpResult
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc.Jsonp
{
    using System;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    /// <summary>
    /// JsonpResult
    /// </summary>
    public class JsonpResult : JsonResult
    {
        /// <summary>
        /// jsonp回调函数名key 
        /// </summary>
        private const string JsonpCallbackName = "callback";

        /// <summary>
        /// CallbackApplicationType
        /// </summary>
        private const string CallbackApplicationType = "application/json";

        /// <summary>
        /// Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult"/> class.
        /// </summary>
        /// <param name="context">
        /// The context within which the result is executed.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="context"/> parameter is null.
        /// </exception>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if ((this.JsonRequestBehavior == JsonRequestBehavior.DenyGet) &&
                  string.Equals(context.HttpContext.Request.HttpMethod, "GET"))
            {
                throw new InvalidOperationException();
            }

            var response = context.HttpContext.Response;

            response.ContentType = !string.IsNullOrEmpty(this.ContentType) ? this.ContentType : CallbackApplicationType;

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data != null)
            {
                string buffer;

                var request = context.HttpContext.Request;
                var serializer = new JavaScriptSerializer();

                if (request[JsonpCallbackName] != null)
                {
                    buffer = string.Format("{0}({1})", request[JsonpCallbackName], serializer.Serialize(this.Data));
                }
                else
                {
                    buffer = serializer.Serialize(this.Data);
                }
               
                response.Write(buffer);
            }
        }
    }
}
