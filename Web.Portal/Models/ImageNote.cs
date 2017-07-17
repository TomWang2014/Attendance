// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageNote.cs" company="zjzx">
//   ©2015 中教在线 版权所有
// </copyright>
// <author>李天赐</author>
// <date>2016/9/20 15:48:01</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Web.Portal.Models
{
    /// <summary>
    /// 头像上传
    /// <remarks>为了满足前端参数，名称改成一致</remarks>
    /// </summary>
    public class ImageNote
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public double height { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public double width { get; set; }

        /// <summary>
        /// Gets or sets the rotate.
        /// </summary>
        public double rotate { get; set; }
    }
}