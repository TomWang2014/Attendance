// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityDtoOfTPrimaryKey.cs" company="WIKI Tec">
//   WIKI Tec copyright.
// </copyright>
// <author>÷Ï–¬¡¡</author>
// <summary>
//   EntityDtoOfTPrimaryKey
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Dto
{
    using System;

    /// <summary>
    /// Implements common properties for entity based DTOs.
    /// </summary>
    /// <typeparam name="TPrimaryKey">Type of the primary key</typeparam>
    [Serializable]
    public class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        /// <summary>
        /// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        /// </summary>
        public EntityDto()
        {
        }

        /// <summary>
        /// Creates a new <see cref="EntityDto{TPrimaryKey}"/> object.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityDto(TPrimaryKey id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Id of the entity.
        /// </summary>
        public TPrimaryKey Id { get; set; }
    }
}
