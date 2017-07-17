// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FromJsonAttribute.cs" company="">
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
    using System.Web.Mvc;

    /// <summary>
    /// 提供JsonModelBinder的CustomModelBinderAttribute
    /// </summary>
    public class FromJsonAttribute : CustomModelBinderAttribute
    {
        /// <summary>
        /// GetBinder
        /// </summary>
        /// <returns>IModelBinder></returns>
        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }
    }
}
