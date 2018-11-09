using System;
using System.Linq.Expressions;

namespace FluentSpec {
    public class Specification<T> : Specification {
        /// <summary>
        /// Represents the Expression Tree evaluating the Specification
        /// </summary>
        public virtual Expression<Func<T, bool>> ExpressionTree { get; }

        public Specification () { }

        internal Specification (Expression<Func<T, bool>> expression) {
            ExpressionTree = expression;
        }

        /// <summary>
        /// Performs a short circuiting conditional AND operation between the current Expression Tree and the given Expression (i.e: If the first operator evaluates to false, the second operator is not evaluated and false is returned)
        /// </summary>
        /// <param name="expression">The target Expression Tree</param>
        /// <returns>A Specification with an Expression Tree representing the short circuiting conditional AND operation between the two trees</returns>
        public Specification<T> AndAlso (Expression<Func<T, bool>> expression) {
            if (expression == default (Expression<Func<T, bool>>)) {
                throw new ArgumentNullException (nameof (expression));
            }

            return new Specification<T> (RenameExpressionParameters<T> (Expression.AndAlso (ExpressionTree.Body, expression.Body)));
        }

        /// <summary>
        /// Performs a short circuiting conditional AND operation between the current Expression Tree and the given Specification's Expression Tree (i.e: If the first operator evaluates to false, the second operator is not evaluated and false is returned)
        /// </summary>
        /// <param name="target">The target Specification</param>
        /// <returns>A Specification with an Expression Tree representing the short circuiting conditional AND operation between the two trees</returns>
        public Specification<T> AndAlso (Specification<T> target) {
            if (target == default (Specification<T>)) {
                throw new ArgumentNullException (nameof (target));
            }

            return AndAlso (target.ExpressionTree);
        }

        /// <summary>
        /// Performs a non-short-circuiting conditional AND operation between the current Expression Tree and the given Expression (i.e: Both expressions are always evaluated)
        /// </summary>
        /// <param name="expression">The target Expression Tree</param>
        /// <returns>A Specification with an Expression Tree representing the non-short-circuiting conditional AND operation between the two trees</returns>
        public Specification<T> And (Expression<Func<T, bool>> expression) {
            if (expression == default (Expression<Func<T, bool>>)) {
                throw new ArgumentNullException (nameof (expression));
            }

            return new Specification<T> (RenameExpressionParameters<T> (Expression.And (ExpressionTree.Body, expression.Body)));
        }

        /// <summary>
        /// Performs a non-short-circuiting conditional AND operation between the current Expression Tree and the given Specification's Expression Tree (i.e: Both expressions are always evaluated)
        /// </summary>
        /// <param name="target">The target Specification</param>
        /// <returns>A Specification with an Expression Tree representing the non-short-circuiting conditional AND operation between the two trees</returns>
        public Specification<T> And (Specification<T> target) {
            if (target == default (Specification<T>)) {
                throw new ArgumentNullException (nameof (target));
            }

            return And (target.ExpressionTree);
        }

        /// <summary>
        /// Performs a short-circuiting conditional OR operation between the current Expression Tree and the given Expression (i.e: If the first operator evaluates to true, the second operator is not evaluated and true is returned)
        /// </summary>
        /// <param name="expression">The target Expression Tree</param>
        /// <returns>A Specification with an Expression Tree representing the short-circuiting conditional OR operation between the two trees</returns>
        public Specification<T> OrElse (Expression<Func<T, bool>> expression) {
            if (expression == default (Expression<Func<T, bool>>)) {
                throw new ArgumentNullException (nameof (expression));
            }

            return new Specification<T> (RenameExpressionParameters<T> (Expression.OrElse (ExpressionTree.Body, expression.Body)));
        }

        /// <summary>
        /// Performs a short-circuiting conditional OR operation between the current Expression Tree and the given Specification's Expression Tree (i.e: If the first operator evaluates to true, the second operator is not evaluated and true is returned)
        /// </summary>
        /// <param name="target">The target Specification</param>
        /// <returns>A Specification with an Expression Tree representing the short-circuiting conditional OR operation between the two trees</returns>
        public Specification<T> OrElse (Specification<T> target) {
            if (target == default (Specification<T>)) {
                throw new ArgumentNullException (nameof (target));
            }

            return OrElse (target.ExpressionTree);
        }

        /// <summary>
        /// Performs a non-short-circuiting conditional OR operation between the current Expression Tree and the given Expression (i.e: Both expressions are always evaluated)
        /// </summary>
        /// <param name="expression">The target Expression Tree</param>
        /// <returns>A Specification with an Expression Tree representing the non-short-circuiting conditional OR operation between the two trees</returns>
        public Specification<T> Or (Expression<Func<T, bool>> expression) {
            if (expression == default (Expression<Func<T, bool>>)) {
                throw new ArgumentNullException (nameof (expression));
            }

            return new Specification<T> (RenameExpressionParameters<T> (Expression.Or (ExpressionTree.Body, expression.Body)));
        }

        /// <summary>
        /// Performs a non-short-circuiting conditional OR operation between the current Expression Tree and the given Specification's Expression Tree (i.e: Both expressions are always evaluated)
        /// </summary>
        /// <param name="target">The target Specification</param>
        /// <returns>A Specification with an Expression Tree representing the non-short-circuiting conditional OR operation between the two trees</returns>
        public Specification<T> Or (Specification<T> target) {
            if (target == default (Specification<T>)) {
                throw new ArgumentNullException (nameof (target));
            }

            return Or (target.ExpressionTree);
        }

        /// <summary>
        /// Performs an Exclusive OR operation between the current Expression Tree and the given Expression
        /// </summary>
        /// <param name="expression">The target Expression Tree</param>
        /// <returns>A Specification with an Expression Tree representing the Exclusive OR operation between the two trees</returns>
        public Specification<T> Xor (Expression<Func<T, bool>> expression) {
            if (expression == default (Expression<Func<T, bool>>)) {
                throw new ArgumentNullException (nameof (expression));
            }

            return new Specification<T> (RenameExpressionParameters<T> (Expression.ExclusiveOr (ExpressionTree.Body, expression.Body)));
        }

        /// <summary>
        /// Performs an Exclusive OR operation between the current Expression Tree and the given Specification's Expression Tree
        /// </summary>
        /// <param name="target">The target Specification</param>
        /// <returns>A Specification with an Expression Tree representing the Exclusive OR operation between the two trees</returns>
        public Specification<T> Xor (Specification<T> target) {
            if (target == default (Specification<T>)) {
                throw new ArgumentNullException (nameof (target));
            }

            return Xor (target.ExpressionTree);
        }

        /// <summary>
        /// Evaluates the Expression Tree against a given argument
        /// </summary>
        /// <param name="arg">The argument that will be used to evaluate the Specification</param>
        /// <returns>A boolean indicating whether the argument satisfies the Specification</returns>
        public virtual bool IsSatisfied (T arg, params object[] args) {
            if (ExpressionTree == default (Expression<Func<T, bool>>)) {
                throw new Exception ("Expression Tree Not Provided.");
            }

            return ExpressionTree.Compile () (arg);
        }
    }
}