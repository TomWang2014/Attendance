// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonModelBinder.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/8/19 13:06:29</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Mvc.ModelBinder
{
    using System.Web.ModelBinding;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    /// <summary>
    /// JsonModelBinder
    /// </summary>
    public class JsonModelBinder : System.Web.Mvc.IModelBinder
    {
        /// <summary>
        /// js序列化器
        /// </summary>
        private readonly static JavaScriptSerializer Serializer = new JavaScriptSerializer();

        /// <summary>
        /// BindModel
        /// </summary>
        /// <param name="controllerContext">
        /// controllerContext 
        /// </param>
        /// <param name="bindingContext">
        /// bindingContext 
        /// </param>
        /// <returns>
        /// model 
        /// </returns>
        public object BindModel(ControllerContext controllerContext, System.Web.Mvc.ModelBindingContext bindingContext)
        {
            var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
            if (string.IsNullOrEmpty(stringified))
            {
                return null;
            }

            var model = Serializer.Deserialize(stringified, bindingContext.ModelType);
            return model;
        }
    }
}
