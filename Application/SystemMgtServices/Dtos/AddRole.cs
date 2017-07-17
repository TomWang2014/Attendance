// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddRole.cs" company="zjzx">
//   2016-11-10 09:37:44
// </copyright>
// <summary>
//   The func small.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Application.SystemMgtServices.Dtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Attendance.Infrastructure.Validation;

    /// <summary>
    /// The func small.
    /// </summary>
    public class AddRole : IValidate
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Required(ErrorMessage = "权限名称不能为空。")]
        public string Name { get; set; }

        /// <summary>
        /// 权限列表
        /// </summary>
        public List<int> SysFuncIds { get; set; }
    }
}