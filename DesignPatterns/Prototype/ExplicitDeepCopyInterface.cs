﻿using System;
namespace DesignPatterns.Prototype.EDCI
{
    // Still same problem with CopyConstructors,
    // there's a need to add a lot of things and it is easy to mess up
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
    public class Person : IPrototype<Person>
    {
        public string[] Names;
        public Address Address;
        public Person(string[] names, Address address)
        {
            if (names == null)
            {
                throw new ArgumentNullException(paramName: nameof(names));
            }
            if (address == null)
            {
                throw new ArgumentNullException(paramName: nameof(address));
            }
            Names = names;
            Address = address;
        }
        public Person(Person other)
        {
            Names = other.Names;
            Address = new Address(other.Address);
        }
        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        public Person DeepCopy()
        {
            return new Person(Names, Address.DeepCopy());
        }
    }
    public class Address : IPrototype<Address>
    {
        public string StreetName;
        public int HouseNumber;
        public Address(string streetName, int houseNumber)
        {
            if (streetName == null)
            {
                throw new ArgumentNullException(paramName: nameof(streetName));
            }
            StreetName = streetName;
            HouseNumber = houseNumber;
        }
        public Address(Address other)
        {
            StreetName = other.StreetName;
            HouseNumber = other.HouseNumber;
        }
        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }

        public Address DeepCopy()
        {
            return new Address(StreetName, HouseNumber);
        }
    }
    public class ExplicitDeepCopyInterface
	{
		public ExplicitDeepCopyInterface()
		{
			var john = new Person(new[] { "John", "Smith" },
				   new Address("London Road", 123));
			var jane = john.DeepCopy();
			jane.Address.HouseNumber = 321;
			Console.WriteLine(john);
			Console.WriteLine(jane);
		}
	}
}

