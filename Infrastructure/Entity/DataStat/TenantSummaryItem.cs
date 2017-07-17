/*****************************************************************************************************
 * 命名空间：Attendance.Infrastructure.Entity.DataStat
 * 版权归zjzx所有
 * 类名称：TenantSummaryItem
 * 文件名：TenantSummaryItem
 * 创建时间：2017/2/23 16:32:33
 * 创建人：zw
 * 创建说明：TenantSummaryItem
*****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Entity.DataStat
{
    public class TenantSummaryItem
    {
        /// <summary>
        /// 合作单位ID
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 合作单位名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Projectname { get; set; }

        /// <summary>
        /// 报名学员数量
        /// </summary>
        public int StudentCount { get; set; }

        /// <summary>
        /// 缴费总额
        /// </summary>
        public decimal PayAmountcount { get; set; }

        /// <summary>
        /// 发证人数
        /// </summary>
        public int HavecerStudent { get; set; }
    }
}
