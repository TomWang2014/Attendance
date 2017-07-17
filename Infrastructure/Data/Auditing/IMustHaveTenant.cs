// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMustHaveTenant.cs" company="zjzx">
//   2016-6-29 09:45:41
// .</copyright>
// <author>李天赐</author>
// <summary>
//   标准化接口，必须有租户id
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Train.Infrastructure.Data.Auditing
{
    /// <summary>
    /// 标准化接口，必须有租户id
    /// Implement this interface for an entity which must have TenantId.
    /// </summary>
    public interface IMustHaveTenant
    {
        /// <summary>
        /// 租户id
        /// TenantId of this entity.
        /// </summary>
        int TenantId { get; set; }
    }
}