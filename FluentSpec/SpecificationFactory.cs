using System;
using System.Collections.Concurrent;

namespace FluentSpec {
    public class SpecificationFactory {
        private static readonly ConcurrentDictionary<Type, Specification> _registeredSpecifications 
            = new ConcurrentDictionary<Type, Specification> ();

        public static T Spec<T> () where T : Specification, new () {
            return (T) _registeredSpecifications.GetOrAdd (typeof (T), _ => new T ());
        }
    }
}