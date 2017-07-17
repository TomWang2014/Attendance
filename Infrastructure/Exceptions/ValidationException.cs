// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationException.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <summary>
//   ValidationException
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    /// <summary>
    /// This exception type is used to throws validation exceptions.
    /// </summary>
    [Serializable]
    public class ValidationException : Exception
    {
        /// <summary>
        /// Detailed list of validation errors for this exception.
        /// </summary>
        public List<ValidationResult> ValidationErrors { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ValidationException()
        {
            this.ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Constructor for serializing.
        /// </summary>
        /// <param name="serializationInfo">
        /// The serialization Info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        public ValidationException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
            this.ValidationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">
        /// Exception message
        /// </param>
        public ValidationException(string message)
            : base(message)
        {
            this.ValidationErrors = new List<ValidationResult>();
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">
        /// Exception message
        /// </param>
        /// <param name="validationErrors">
        /// Validation errors
        /// </param>
        public ValidationException(string message, List<ValidationResult> validationErrors)
            : base(message)
        {
            this.ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">
        /// Exception message
        /// </param>
        /// <param name="innerException">
        /// Inner exception
        /// </param>
        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
            this.ValidationErrors = new List<ValidationResult>();
        }
    }
}
