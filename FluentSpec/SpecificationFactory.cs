using System;
using System.Collections.Concurrent;

namespace FluentSpec {
    public static class SpecificationFactory {
        private static readonly ConcurrentDictionary<Type, Specification> _registeredSpecifications 
                = new ConcurrentDictionary<Type, Specification> ();

        public static T Default<T> () where T : Specification, new () {
            return (T) _registeredSpecifications.GetOrAdd (typeof (T), _ => new T ());
        }
    }
}