using System;
using System.Linq.Expressions;

namespace FluentSpec.Tests {
    public class ValidCustomerNameSpecification : Specification<Customer> {
        public ValidCustomerNameSpecification () : base (x => x.Name.Length > 0 && x.Name.Length < 10) { }
    }
}