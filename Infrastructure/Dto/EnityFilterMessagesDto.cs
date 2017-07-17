// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnityFilterMessagesDto.cs" company="zjzx">
//   2016-11-18 10:47:14
// </copyright>
// <summary>
//   The messages.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Dto
{
    using System;

    /// <summary>
    /// The messages.
    /// </summary>
    public class EnityFilterMessagesDto
    {
        /// <summary>
        /// The exception_ invalid page index.
        /// </summary>
        public static string ExceptionInvalidPageIndex;

        /// <summary>
        /// The exception_ filter cannot be null.
        /// </summary>
        public static Exception ExceptionFilterCannotBeNull;

        /// <summary>
        /// The exception_ invalid page size.
        /// </summary>
        public static string ExceptionInvalidPageSize;

        /// <summary>
        /// The exception_ order by expression cannot be null.
        /// </summary>
        public static Exception ExceptionOrderByExpressionCannotBeNull;
    }
}