using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DesignPatterns.Prototype.CTS
{
    public static class ExtensionMethods
    {
        // Binary is fast but class must be [Serializable]
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }
        // Xml will ask for a parameterless constructor
        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }
    }
    [Serializable] // -> for binary
    public class Person
    {
        public string[] Names;
        public Address Address;
        public Person() { } // -> for xml
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
    [Serializable] // -> for binary
    public class Address
    {
        public string StreetName;
        public int HouseNumber;
        public Address() { } // -> for xml
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
    public class CopyThroughSerialization
	{
		public CopyThroughSerialization()
		{
            var john = new Person(new[] { "John", "Smith" },
                      new Address("London Road", 123));
            var jane = john.DeepCopyXml();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 321;
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
	}
}

