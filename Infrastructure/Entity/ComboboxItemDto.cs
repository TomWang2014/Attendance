// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComboboxItemDto.cs" company="">
//   
// </copyright>
// <author>¿ÓÃÏ¥Õ</author>
// <summary>
//   ComboboxItemDto
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Entity
{
    /// <summary>
    /// This DTO can be used as a simple item for a combobox/list.
    /// </summary>    
    public class ComboboxItemDto
    {
        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// </summary>
        public ComboboxItemDto()
        {
        }

        /// <summary>
        /// Creates a new <see cref="ComboboxItemDto"/>.
        /// </summary>
        /// <param name="value">
        /// Value of the item
        /// </param>
        /// <param name="displayText">
        /// Display text of the item
        /// </param>
        public ComboboxItemDto(string value, string displayText)
        {
            this.Value = value;
            this.DisplayText = displayText;
        }

        /// <summary>
        /// Value of the item.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Display text of the item.
        /// </summary>
        public string DisplayText { get; set; }
    }
}