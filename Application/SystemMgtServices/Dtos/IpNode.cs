
using Domain;
using Attendance.Infrastructure.AutoMapper;
using Attendance.Infrastructure.Dto;
namespace Attendance.Application.SystemMgtServices.Dtos
{

    /// <summary>
    /// IpNode
    /// </summary>
    [AutoMap(typeof(NetTenantIpWhite))]
    public class IpNode : EntityDto
    {
        /// <summary>
        /// Gets or sets the tenant id.
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        public string IpAddress { get; set; }
    }
}
