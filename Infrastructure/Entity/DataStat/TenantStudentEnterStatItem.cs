/*****************************************************************************************************
 * 命名空间：Attendance.Infrastructure.Entity.DataStat
 * 版权归zjzx所有
 * 类名称：TenantStudentEnterStatItem
 * 文件名：TenantStudentEnterStatItem
 * 创建时间：2017/2/23 11:04:53
 * 创建人：zw
 * 创建说明：TenantStudentEnterStatItem
*****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Entity.DataStat
{
    public class TenantStudentEnterStatItem
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string TenantName { get; set; }

        /// <summary>
        /// 报名学员数量
        /// </summary>
        public int StudentCount { get; set; }
    }
}
