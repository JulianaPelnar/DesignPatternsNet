using System;
namespace DesignPatterns.Builder.FunctionalBuilder
{
	public class Person
    {
		public string Name, Position;
    }

	public abstract class FunctionalBuilder<TSubject, TSelf>
		where TSelf : FunctionalBuilder<TSubject, TSelf>
		where TSubject : new()
    {
        private readonly List<Func<Person, Person>> actions
            = new List<Func<Person, Person>>(); // Fluent
        public TSelf Do(Action<Person> action)
            => AddAction(action);
        public Person Build()
            => actions.Aggregate(new Person(), (p, f) => f(p));
        private TSelf AddAction(Action<Person> action)
        {
            actions.Add(p =>
            {
                action(p);
                return p;
            });
            return (TSelf)this;
        }
    }

	public sealed class PersonBuilder
		: FunctionalBuilder<Person, PersonBuilder>
    {
		public PersonBuilder Called(string name)
			=> Do(p => p.Name = name);
    }
	// This is commented to show both ways to do the same, although the
	// uncommented is preferred
	//public sealed class PersonBuilder // sealed means you can't inherit from it
	//   {
	//	private readonly List<Func<Person, Person>> actions
	//		= new List<Func<Person, Person>>(); // Fluent
	//	public PersonBuilder Called(string name)
	//		=> Do(p => p.Name = name);
	//	public PersonBuilder Do(Action<Person> action)
	//		=> AddAction(action);
	//	public Person Build()
	//		=> actions.Aggregate(new Person(), (p, f) => f(p));
	//	private PersonBuilder AddAction(Action<Person> action)
	//       {
	//		actions.Add(p =>
	//		{
	//			action(p);
	//			return p;
	//		});
	//		return this;
	//       }
	//   }
	public static class PersonBuilderExtensions
    {
		public static PersonBuilder WorkAs
			(this PersonBuilder builder, string position)
			=> builder.Do(p => p.Position = position);
    }
	public class FunctionalBuilder
	{
		public FunctionalBuilder()
		{
			var person = new PersonBuilder()
				.Called("Sarah")
				.WorkAs("Developer")
				.Build();
			Console.WriteLine($"Name: {person.Name}, Position: {person.Position}");
		}
	}
}

