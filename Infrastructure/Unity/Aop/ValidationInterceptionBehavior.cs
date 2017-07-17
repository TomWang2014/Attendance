// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationInterceptionBehavior.cs" company="">
//   
// </copyright>
// <author>李天赐</author>
// <date>2015/4/2 11:01:53</date>
// <summary>
//   主要功能有：
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Attendance.Infrastructure.Unity.Aop
{
    using System;
    using System.Collections.Generic;

    using Attendance.Infrastructure.Validation;

    using Microsoft.Practices.Unity.InterceptionExtension;

    /// <summary>
    /// ValidationInterceptionBehavior
    /// </summary>
    public class ValidationInterceptionBehavior : IInterceptionBehavior
    {
        /// <summary>
        /// The invoke.
        /// </summary>
        /// <param name="input">
        /// The input. 
        /// </param>
        /// <param name="getNext">
        /// The get next. 
        /// </param>
        /// <returns>
        /// The <see cref="IMethodReturn"/>. 
        /// </returns>
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            // Before invoking the method on the original target.
            var list = new List<object>();
            foreach (var argument in input.Arguments)
            {
                list.Add(argument);
            }

            var group = new MethodInvocationValidator(input.MethodBase, list.ToArray());
            group.Validate();

            // Invoke the next behavior in the chain.
            var result = getNext()(input, getNext);

            // After invoking the method on the original target.
            return result;
        }

        /// <summary>
        /// The get required interfaces.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        /// <summary>
        /// Gets a value indicating whether will execute.
        /// </summary>
        public bool WillExecute
        {
            get { return true; }
        }
    }
}
