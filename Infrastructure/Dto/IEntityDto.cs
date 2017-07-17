// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntityDto.cs" company="WIKI Tec">
//   WIKI Tec copyright.
// </copyright>
// <author>÷Ï–¬¡¡</author>
// <summary>
//   IEntityDto
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Dto
{
    /// <summary>
    /// A shortcut of <see cref="IEntityDto{TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    public interface IEntityDto : IEntityDto<int>
    {
    }
}