// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntityDtoOfPrimaryKey.cs" company="WIKI Tec">
//   WIKI Tec copyright.
// </copyright>
// <author>朱新亮</author>
// <summary>
//   IEntityDtoOfPrimaryKey
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Dto
{
    /// <summary>
    /// Defines common properties for entity based DTOs.
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface IEntityDto<TPrimaryKey> : IDto
    {
        /// <summary>
        /// Id of the entity.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}