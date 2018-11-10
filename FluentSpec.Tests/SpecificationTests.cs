using System;
using System.Linq.Expressions;
using Xunit;
using static FluentSpec.Specification;
using static FluentSpec.SpecificationFactory;

namespace FluentSpec.Tests
{
    public class SpecificationTests
    {
        [Fact]
        public void IsSatisfied_NoExpressionTree_ThrowsException()
        {
            var customer = new Customer("test", DateTime.Now);
            Assert.Throws<Exception>(() => Default<NoExpressionTreeSpecification>().IsSatisfied(customer));
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("specification test specification test", false)]
        [InlineData("test", true)]
        public void IsSatisfied_ValidTree_EvaluatesSpecification(string name, bool expected)
        {
            var customer = new Customer(name, DateTime.Now);
            var spec = Default<ValidCustomerNameSpecification>();
            var evaluation = spec.IsSatisfied(customer);
            Assert.Equal(expected, evaluation);
        }

        [Fact]
        public void AndExpression_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Default<AdultCustomerSpecification>()
               .And(default(Specification<Customer>)));
        }

        [Fact]
        public void AndSpecification_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Default<AdultCustomerSpecification>()
               .And(default(Expression<Func<Customer, bool>>)));
        }

        [Theory]
        [InlineData("", 1991, false)]
        [InlineData("test", 2018, false)]
        [InlineData("", 2018, false)]
        [InlineData("test", 1991, true)]
        public void AndSpecification_NotNull_EvaluatesConditionalAndOperation(string name, int year, bool expected)
        {
            var customer = new Customer(name, new DateTime(year, 1, 1));
            var spec = Default<ValidCustomerNameSpecification>().And(Default<AdultCustomerSpecification>());
            var evaluation = spec.IsSatisfied(customer);
            Assert.Equal(expected, evaluation);
        }

        [Fact]
        public void OrExpression_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Default<AdultCustomerSpecification>()
               .Or(default(Specification<Customer>)));
        }

        [Fact]
        public void OrSpecification_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Default<AdultCustomerSpecification>()
               .Or(default(Expression<Func<Customer, bool>>)));
        }

        [Theory]
        [InlineData("", 1991, true)]
        [InlineData("test", 2018, true)]
        [InlineData("", 2018, false)]
        [InlineData("test", 1991, true)]
        public void OrSpecification_NotNull_EvaluatesConditionalOrOperation(string name, int year, bool expected)
        {
            var customer = new Customer(name, new DateTime(year, 1, 1));
            var spec = Default<ValidCustomerNameSpecification>().Or(Default<AdultCustomerSpecification>());
            var evaluation = spec.IsSatisfied(customer);
            Assert.Equal(expected, evaluation);
        }

        [Fact]
        public void XorExpression_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => new AdultCustomerSpecification()
               .And(default(Specification<Customer>)));
        }

        [Fact]
        public void XorSpecification_Null_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => Default<AdultCustomerSpecification>()
               .And(default(Expression<Func<Customer, bool>>)));
        }

        [Theory]
        [InlineData("", 2018, false)]
        [InlineData("", 1991, true)]
        [InlineData("test", 2018, true)]
        [InlineData("test", 1991, false)]
        public void XorSpecification_NotNull_EvaluatesConditionalOrOperation(string name, int year, bool expected)
        {
            var customer = new Customer(name, new DateTime(year, 1, 1));
            var spec = Default<ValidCustomerNameSpecification>().Xor(Default<AdultCustomerSpecification>());
            var evaluation = spec.IsSatisfied(customer);
            Assert.Equal(expected, evaluation);
        }

        [Fact]
        public void NotSpecification_Null_ThrowsException()
        {
            Assert.Throws<Exception>(() => Not(Default<NoExpressionTreeSpecification>()));
        }

        [Theory]
        [InlineData(1991, false)]
        [InlineData(2018, true)]
        public void NotSpecification_NotNull_EvaluatesNotOperation(int year, bool expected)
        {
            var customer = new Customer("test", new DateTime(year, 1, 1));
            var spec = Not(Default<AdultCustomerSpecification>());
            var evaluation = spec.IsSatisfied(customer);
            Assert.Equal(expected, evaluation);
        }

        [Theory]
        [InlineData("test1234567", 1991, 1, true)]
        [InlineData("test", 1991, 1, true)]
        [InlineData("test", 1991, 20, true)]
        [InlineData("test", 2018, 20, false)]
        public void ChainedSpecifications_Valid_EvaluateOperation(string name, int yearOfBirth, int memberSince, bool expected)
        {
            var customer = new Customer(name, new DateTime(yearOfBirth, 1, 1),
                DateTime.Now.AddYears(-1 * memberSince));
            var spec = (Default<AdultCustomerSpecification>().And(Default<ValidCustomerNameSpecification>()))
                .Or(Not(Default<PremiumCustomerSpecification>()));
            var evaluation = spec.IsSatisfied(customer);
            Assert.Equal(expected, evaluation);
        }
    }
}