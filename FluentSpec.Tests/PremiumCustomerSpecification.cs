using System;
using System.Linq.Expressions;

namespace FluentSpec.Tests {
    public class PremiumCustomerSpecification : Specification<Customer> {
        public override Expression<Func<Customer, bool>> ExpressionTree { get; } = x => DateTime.Now.Year - x.MemberSince.Year > 10;
    }
}