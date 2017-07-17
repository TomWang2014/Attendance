// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMayHaveTenant.cs" company="zjzx">
//   
// .</copyright>
// <author>李天赐</author>
// <summary>
//   标准化接口，可能有租户id
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Train.Infrastructure.Domain.Entities
{
    /// <summary>
    /// 标准化接口，可能有租户id
    /// Implement this interface for an entity which may optionally have TenantId.
    /// </summary>
    public interface IMayHaveTenant
    {
        /// <summary>
        /// 租户id
        /// TenantId of this entity.
        /// </summary>
        int? TenantId { get; set; }
    }
}