using System;
namespace DesignPatterns.Prototype.CopyConstructors
{
    // A pattern used in c++, it's better than ICloneable, but still a bit messy.
    // It's also not very idiomatic and not many people would recognize it.
    public class Person
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
    }
    public class Address
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
    }
    public class CopyConstructors
    {
        public CopyConstructors()
        {
            var john = new Person(new[] { "John", "Smith" },
                new Address("London Road", 123));
            var jane = new Person(john);
            jane.Address.HouseNumber = 321;
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}

