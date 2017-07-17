// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonHelper.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2014/6/16 12:50:16</date>
// <summary>
//   Json工具类
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Toolkit
{
    using System.Web.Script.Serialization;

    /// <summary>
    /// Json工具类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将对象转化为Json字符串   
        /// </summary>
        /// <typeparam name="T">
        /// T
        /// </typeparam>
        /// <param name="obj">
        /// 源对象
        /// </param>
        /// <returns>
        /// json数据
        /// </returns>
        public static string Obj2JsonStr<T>(T obj)
        {
            var serialize = new JavaScriptSerializer();
            return serialize.Serialize(obj);
        }

        /// <summary>
        /// 将Json字符串转化为对象  
        /// </summary>
        /// <param name="strJson">
        /// Json字符串
        /// </param>
        /// <returns>
        /// 目标对象
        /// </returns>
        public static T JsonStr2Obj<T>(string strJson)
        {
            var serialize = new JavaScriptSerializer();
            return serialize.Deserialize<T>(strJson);
        }
    }
}