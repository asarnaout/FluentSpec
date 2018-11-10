using System;
using System.Linq.Expressions;

namespace FluentSpec {
    public abstract class Specification {

        internal Specification () { }

        /// <summary>
        /// Performs a NOT operation on the Expression Tree
        /// </summary>
        /// <param name="spec">The specification to be negated</param>
        /// <returns>A Specification with an Expression Tree representing a negation of the original Specification</returns>
        public static Specification<T> Not<T> (Specification<T> spec) {
            if (spec.ExpressionTree == default (Expression<Func<T, bool>>)) {
                throw new Exception ("Expression Tree Not Provided.");
            }

            return new Specification<T> (RenameExpressionParameters<T> (Expression.Not (spec.ExpressionTree.Body)));
        }

        protected static Expression<Func<T, bool>> RenameExpressionParameters<T> (Expression expr) {
            var param = Expression.Parameter (typeof (T));
            return new ParameterExpressionVisitor (param).Visit (Expression.Lambda<Func<T, bool>> (expr, param))
            as Expression<Func<T, bool>>;
        }
    }
}