using System;
namespace DesignPatterns.CodingExercises
{
	// Please implement a non-static PersonFactory that has a CreatePerson()
	// method that takes a person's name.
	// The Id of the person should be set as a 0-based index of the object created.
	public class Person
    {
		public int Id { get; set; }
		public string Name { get; set; }
	}
	public class PersonFactory
	{
		private readonly List<Person> people = new List<Person>();
		public Person CreatePerson(string name)
		{
			Person person = new Person();
			person.Name = name;
			person.Id = people.Count;
			people.Add(person);
			return person;
		}
	}
	public class Factory
	{
		public Factory()
		{
			PersonFactory factory = new PersonFactory();
			Person a = factory.CreatePerson("Aria");
			Person b = factory.CreatePerson("Athanasia");
			Person c = factory.CreatePerson("Melissa");
			Person d = factory.CreatePerson("Jake");
			Console.WriteLine(a.Id.ToString() + " " + b.Id.ToString() + " " + c.Id.ToString() + " " + d.Id.ToString() + " ");
		}
	}
}

