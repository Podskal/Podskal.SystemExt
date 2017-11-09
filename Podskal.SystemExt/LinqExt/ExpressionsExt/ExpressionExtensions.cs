using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SystemExt.LinqExt.ExpressionsExt
{
    /// <summary>
    /// Contains some extension and helper methods for the <see cref="System.Linq.Expressions.Expression"/> instances.
    /// </summary>
    public static class ExpressionExtensions
    {
        #region Methods

        /// <summary>
        /// Makes an expression that adds two elements of type <typeparamref name="T"/>.
        /// </summary>
        public static Expression<Func<T, T, T>> MakeAddExpression<T>()
        {
            var parameters = new[]
            {
                Expression.Parameter(typeof(T)),
                Expression.Parameter(typeof(T))
            };

            return Expression
                .Lambda<Func<T, T, T>>(
                    Expression.Add(
                        parameters[0], 
                        parameters[1]),
                    parameters);
        }

        #endregion
    }
}
