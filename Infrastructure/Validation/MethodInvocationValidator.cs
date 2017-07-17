// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MethodInvocationValidator.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2016/7/6 17:17:43</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Validation
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    using Attendance.Infrastructure.Collections.Extensions;
    using Attendance.Infrastructure.Exceptions;

    /// <summary>
    ///  This class is used to validate a method call (invocation) for method arguments.
    /// </summary>
    internal class MethodInvocationValidator
    {
        /// <summary>
        /// The parameter values.
        /// </summary>
        private readonly object[] parameterValues;

        /// <summary>
        /// The _parameters.
        /// </summary>
        private readonly ParameterInfo[] parameters;

        /// <summary>
        /// The _validation errors.
        /// </summary>
        private readonly List<ValidationResult> validationErrors;

        /// <summary>
        /// Creates a new MethodInvocationValidator instance.
        /// </summary>
        /// <param name="method">
        /// Method to be validated
        /// </param>
        /// <param name="parameterValues">
        /// List of arguments those are used to call the <paramref name="method"/>.
        /// </param>
        public MethodInvocationValidator(MethodBase method, object[] parameterValues)
        {
            this.parameterValues = parameterValues;
            this.parameters = method.GetParameters();
            this.validationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// Validates the method invocation.
        /// </summary>
        public void Validate()
        {
            if (this.parameters.IsNullOrEmpty())
            {
                // Object has no parameter, no need to validate.
                return;
            }

            if (this.parameters.Length != this.parameterValues.Length)
            {
                // his is not possible actually
                throw new Exception("Method parameter count does not match with argument count!");
            }

            for (var i = 0; i < this.parameters.Length; i++)
            {
                this.ValidateMethodParameter(this.parameters[i], this.parameterValues[i]);
            }

            if (this.validationErrors.Any())
            {
                // throw new ValidationException("方法参数无效！详细请参考ValidationErrors.") { ValidationErrors = _validationErrors };
                var errorBuilder = new StringBuilder();
                foreach (var error in this.validationErrors)
                {
                    errorBuilder.AppendFormat("{0}", error.ErrorMessage);
                }

                throw new UserFriendlyException(errorBuilder.ToString());
            }

            foreach (var parameterValue in this.parameterValues)
            {
                NormalizeParameter(parameterValue);
            }
        }

        /// <summary>
        /// Validates given parameter for given value.
        /// </summary>
        /// <param name="parameterInfo">
        /// Parameter of the method to validate
        /// </param>
        /// <param name="parameterValue">
        /// Value to validate
        /// </param>
        private void ValidateMethodParameter(ParameterInfo parameterInfo, object parameterValue)
        {
            if (parameterValue == null)
            {
                if (!parameterInfo.IsOptional &&
                    !parameterInfo.IsOut && !IsNullableType(parameterInfo.ParameterType))
                {
                    this.validationErrors.Add(new ValidationResult(parameterInfo.Name + " is null!", new[] { parameterInfo.Name }));
                }

                return;
            }

            this.ValidateObjectRecursively(parameterValue);
        }

        /// <summary>
        /// The validate object recursively.
        /// </summary>
        /// <param name="validatingObject">
        /// The validating object.
        /// </param>
        private void ValidateObjectRecursively(object validatingObject)
        {
            if (validatingObject is IEnumerable)
            {
                foreach (var item in validatingObject as IEnumerable)
                {
                    this.ValidateObjectRecursively(item);
                }
            }

            if (!(validatingObject is IValidate))
            {
                return;
            }

            this.SetValidationAttributeErrors(validatingObject);

            if (validatingObject is ICustomValidate)
            {
                (validatingObject as ICustomValidate).AddValidationErrors(this.validationErrors);
            }

            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                this.ValidateObjectRecursively(property.GetValue(validatingObject));
            }
        }

        /// <summary>
        /// Checks all properties for DataAnnotations attributes.
        /// </summary>
        /// <param name="validatingObject">
        /// The validating Object.
        /// </param>
        private void SetValidationAttributeErrors(object validatingObject)
        {
            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties)
            {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty())
                {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject) { DisplayName = property.Name };
                foreach (var attribute in validationAttributes)
                {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);

                    if (result != null)
                    {
                        this.validationErrors.Add(result);
                    }
                }
            }
        }

        /// <summary>
        /// The normalize parameter.
        /// </summary>
        /// <param name="parameterValue">
        /// The parameter value. 
        /// </param>
        private void NormalizeParameter(object parameterValue)
        {
            if (parameterValue is IShouldNormalize)
            {
                (parameterValue as IShouldNormalize).Normalize();
            }
        }

        /// <summary>
        /// 验证类型是否是Nullable类型
        /// </summary>
        /// <param name="theType">
        /// type
        /// </param>
        /// <returns>
        /// 是否是nullable
        /// </returns>
        private bool IsNullableType(Type theType)
        {
            return theType.IsGenericType &&
                theType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
