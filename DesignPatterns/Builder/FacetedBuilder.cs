using System;
namespace DesignPatterns.Builder.FacetedBuilder
{
	public class Person
    {
		// address
		public string StreetAddress, Postcode, City;
		// employment
		public string CompanyName, Position;
		public int AnnualIncome;
        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, " +
                $"{nameof(Postcode)}: {Postcode}, " +
                $"{nameof(City)}: {City}, " +
                $"{nameof(CompanyName)}: {CompanyName}, " +
                $"{nameof(Position)}: {Position}, " +
                $"{nameof(AnnualIncome)}: {AnnualIncome}, ";
        }
    }
	public class PersonBuilder // facade
    {
        // reference
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
        // To be able to get an actual Person from the builder:
        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }
    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }
        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }
        public PersonAddressBuilder WithPostCode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }
        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }
    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }
        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }
        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }
        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }
	public class FacetedBuilder
	{
		public FacetedBuilder()
		{
            var pb = new PersonBuilder();
            Person person = pb
                .Lives.At("123 London Road").In("London").WithPostCode("SW12AC")
                .Works.At("IKEA").AsA("Engineer").Earning(123000);
            Console.WriteLine(person);
		}
	}
}

