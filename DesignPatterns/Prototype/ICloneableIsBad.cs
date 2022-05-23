using System;
namespace DesignPatterns.Prototype
{
    // since prototype is about deep copy and ICloneable does shallow copy,
    // this is not a suitable interface to implement this pattern
    public class Person : ICloneable
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

        public object Clone()
        {
            // it is a shallow copy, gets Address by reference
            //return new Person(Names, Address);
            // this line fixes it by getting a clone of Address, but it's still not preferable
            return new Person(Names, (Address) Address.Clone());
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }
    public class Address : ICloneable
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
        // For Person to be able to get the clone with the value.
        // Will return both values and fix the problem
        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }
    public class ICloneableIsBad
    {
        public ICloneableIsBad()
        {
            var john = new Person(new[] { "John", "Smith" },
                new Address("London Road", 123));
            var jane = (Person)john.Clone();
            jane.Address.HouseNumber = 321;
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}

