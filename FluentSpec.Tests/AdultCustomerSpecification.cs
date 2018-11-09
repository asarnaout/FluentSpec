using System;
using System.Linq.Expressions;

namespace FluentSpec.Tests {
    public class AdultCustomerSpecification : Specification<Customer> {
        public override Expression<Func<Customer, bool>> ExpressionTree { get; } = x => DateTime.Now.Year - x.DateOfBirth.Year >= 21;
    }
}