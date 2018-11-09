using System;

namespace FluentSpec.Tests {
    public class Customer {
        public Customer (string name, DateTime dateOfBirth) {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public Customer (string name, DateTime dateOfBirth, DateTime memberSince) : this (name, dateOfBirth) {
            MemberSince = memberSince;
        }

        public DateTime MemberSince { get; }

        public string Name { get; }

        public DateTime DateOfBirth { get; }
    }
}