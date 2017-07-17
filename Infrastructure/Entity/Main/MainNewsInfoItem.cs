using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Entity.Main
{
    public class MainNewsInfoItem
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop { get; set; }

        /// <summary>
        /// 是否发布
        /// </summary>
        public int IsPublish { get; set; }

        /// <summary>
        /// 新闻类别名称
        /// </summary>
        public string NewsTypeName { get; set; }
    }
}
