// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterRebinder.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ParameterRebinder type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Attendance.Infrastructure.Data.Specification
{
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The parameter rebinder.
    /// </summary>
    public class ParameterRebinder : ExpressionVisitor
    {
        /// <summary>
        /// The _map.
        /// </summary>
        private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterRebinder"/> class.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
        {
            this._map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
        }

        /// <summary>
        /// The replace parameters.
        /// </summary>
        /// <param name="map">
        /// The map.
        /// </param>
        /// <param name="exp">
        /// The exp.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
        {
            return new ParameterRebinder(map).Visit(exp);
        }

        /// <summary>
        /// The visit parameter.
        /// </summary>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <returns>
        /// The <see cref="Expression"/>.
        /// </returns>
        protected override Expression VisitParameter(ParameterExpression node)
        {
            ParameterExpression replacement;
            if (this._map.TryGetValue(node, out replacement))
                node = replacement;

            return base.VisitParameter(node);
        }
    }
}
