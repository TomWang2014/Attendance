// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeEx.cs" company="">
//   
// </copyright>
// <author>Zw</author>
// <date>2016/7/5 10:31:23</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Toolkit
{
    using System;

    /// <summary>
    /// The date time ex.
    /// </summary>
    public static class DateTimeEx
    {
        /// <summary>
        /// 时间起始位
        /// </summary>
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// 时差长
        /// </summary>
        private static readonly TimeSpan BeijingTimeOffset = new TimeSpan(8, 0, 0);

        /// <summary>
        /// 得到当前时间的时间戳
        /// </summary>
        public static long CurrentUnixTimestamp
        {
            get
            {
                return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
            }
        }

        /// <summary>
        /// 时间戳转北京时间
        /// </summary>
        /// <param name="unixTimestamp">
        /// 时间戳
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime ToBeijingTime(this long unixTimestamp)
        {
            return UnixEpoch.AddSeconds(unixTimestamp).UtcTime2BeijingTime();
        }

        /// <summary>
        /// 日期转时间戳
        /// </summary>
        /// <param name="datetime">
        /// 时间
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long ToUnixTime(this DateTime datetime)
        {
            return (long)(datetime - UnixEpoch).TotalSeconds;
        }

        /// <summary>
        /// 根据日期得到北京时间(失去差异)
        /// </summary>
        /// <param name="dateTime">
        /// 时间
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public static DateTime UtcTime2BeijingTime(this DateTime dateTime)
        {
            if (dateTime < DateTime.MaxValue - BeijingTimeOffset)
                return dateTime + BeijingTimeOffset;
            return DateTime.MaxValue;
        }

        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="dateTime">
        /// 时间
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="dateTime">
        /// 时间
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateTimeStringNoMill(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm");
        }

        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="dateTime">
        /// 时间
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy年MM月dd日");
        }


        /// <summary>
        /// 时间格式化
        /// </summary>
        /// <param name="dateTime">
        /// 时间
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateZhString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="dateTime">
        /// 对比时间
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToDateDiff(this DateTime dateTime)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = DateTime.Now - dateTime;
                if (ts.Days >= 1)
                {
                    dateDiff = dateTime.Month + "月" + dateTime.Day + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
    }
}
