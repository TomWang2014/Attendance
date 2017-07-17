// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityDto.cs" company="WIKI Tec">
//   WIKI Tec copyright.
// </copyright>
// <author>÷Ï–¬¡¡</author>
// <summary>
//   EntityDto
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Dto
{
    using System;

    /// <summary>
    /// A shortcut of <see cref="EntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    [Serializable]
    public class EntityDto : EntityDto<int>, IEntityDto
    {
        /// <summary>
        /// Creates a new <see cref="EntityDto"/> object.
        /// </summary>
        public EntityDto()
        {
        }

        /// <summary>
        /// Creates a new <see cref="EntityDto"/> object.
        /// </summary>
        /// <param name="id">Id of the entity</param>
        public EntityDto(int id)
            : base(id)
        {
        }
    }
}