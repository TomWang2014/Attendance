// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeFormatDto.cs" company="">
//   
// </copyright>
// <author>朱新亮</author>
// <summary>
//   NameValueDto
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Entity
{
    using System;

    /// <summary>
    /// Can be used to send/receive Name/Value (or Key/Value) pairs.
    /// </summary>
    [Serializable]
    public class DateTimeFormatDto
    {
        /// <summary>
        /// Creates a new <see cref="NameValueDto"/>.
        /// </summary>
        public DateTimeFormatDto()
        {
        }

        /// <summary>
        /// 代表格式 yyyy/MM/dd
        /// </summary>
        public string I { get; set; }

        /// <summary>
        /// 代表格式 HH:mm
        /// </summary>
        public string M { get; set; }

        /// <summary>
        /// 代表格式 yyy-MM-dd HH:mm
        /// </summary>
        public string S { get; set; }
    }
}