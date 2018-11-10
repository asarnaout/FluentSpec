using System;
using System.Linq.Expressions;

namespace FluentSpec.Tests {
    public class PremiumCustomerSpecification : Specification<Customer> {
        public PremiumCustomerSpecification () : base (x => DateTime.Now.Year - x.MemberSince.Year > 10) { }
    }
}