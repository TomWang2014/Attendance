using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Entity.Main
{
    public class MainProject
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 课时
        /// </summary>
        public int LearnTime { get; set; }

        /// <summary>
        /// 老师
        /// </summary>
        public string Teacher { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string FrontImage { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Shortabstract { get; set; }

        /// <summary>
        /// 项目url
        /// </summary>
        public string Url { get; set; }
    }
}
