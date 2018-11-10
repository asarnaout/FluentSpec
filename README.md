A simple library providing an implementation for the Specification Pattern and supporting Composite Specifications through Fluent APIs.

## Specification Pattern:

Specifications are value objects that are capable of verifying whether an argument fits specific criteria. Specifications are mainly used in:

* Verifying Business Rules
* Querying Data
* Generating Data

Specifications are best used in C# to encapsulate complex LINQ expressions that are well hidden and duplicated across the codebase. Using Specifications in conjunction with Repositories allows for a simpler Repository interface. 

Note that Specifications should be only used when necessary and abusing this pattern would highly increase the code complexity.

## Usage:

To define a Specification, simply declare a class and extend the `Specification<T>` type. The extended class should feed the Expression Tree representing the specification to the constructor.

```csharp
public class PremiumCustomerSpecification : Specification<Customer> 
{
    public PremiumCustomerSpecification () : base (x => DateTime.Now.Year - x.MemberSince.Year > 10) 
    { 
    }
}
```

To minimize the number of created objects, a Flyweight Factory `SpecificationFactory` is provided to serve the required Specifications:

```csharp
using static FluentSpec.SpecificationFactory;

public class SpecificationDemo 
{
  public void Test()
  {
    var spec = Default<PremiumCustomerSpecification>();
  }
}
```

Note that clients are allowed to bypass the `SpecificationFactory` and directly create new instances of their Specifications in code.

Use the `IsSatisfied` method to verify whether an argument meets the criterion specified in `ExpressionTree`

```csharp
var spec = Default<PremiumCustomerSpecification>();
var customer = new Customer();
var isSatisfied = spec.IsSatisfied(customer);
```

Chain Specifications to produce more complex Specifications

```csharp
var spec = Not(Default<AdultCustomerSpecification>()).And(Default<ValidCustomerNameSpecification>())    
                             .Or(Default<PremiumCustomerSpecification>());
```

Note that Specifications are immutable Value Objects, and thus any chaining operation would result in a new Specification object being spawned.