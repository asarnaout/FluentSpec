using System;
using System.Linq.Expressions;

namespace FluentSpec.Tests {
    public class AdultCustomerSpecification : Specification<Customer> {
        public AdultCustomerSpecification () : base (x => DateTime.Now.Year - x.DateOfBirth.Year >= 21) { }
    }
}