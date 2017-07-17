/*****************************************************************************************************
 * 命名空间：Attendance.Infrastructure.Entity.DataStat
 * 版权归zjzx所有
 * 类名称：TenantProjectStatItem
 * 文件名：TenantProjectStatItem
 * 创建时间：2017/2/23 10:29:03
 * 创建人：zw
 * 创建说明：TenantProjectStatItem
*****************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attendance.Infrastructure.Entity.DataStat
{
    public class TenantProjectStatItem
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
        /// 项目数量
        /// </summary>
        public int ProjectCount { get; set; }

    }
}
