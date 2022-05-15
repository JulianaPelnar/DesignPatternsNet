using System;
namespace DesignPatterns.Builder
{
	public class Person
    {
		public string Name;
		public string Position;
		public class Builder : PersonJobBuilder<Builder> { }
		public static Builder New => new Builder();
        public override string ToString()
        {
			return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }
	public abstract class PersonBuilder
    {
		protected Person person = new Person();
		public Person Build()
        {
			return person;
        }
    }
	// class Foo : Bar<Foo>
	public class PersonInfoBuilder<SELF> : PersonBuilder
		where SELF : PersonInfoBuilder<SELF>
    {
		public SELF Called(string name)
        {
			person.Name = name;
			return (SELF)this;
        }
    }
	public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
		where SELF : PersonJobBuilder<SELF>
    {
		public SELF WorkAsA(string position)
        {
			person.Position = position;
			return (SELF)this;
        }
    }
	// The problem with fluent builder is that you are not allowed to use the
	// containing type as the return type
	// To avoid this, recursive generics are used (mantain the
	// open-closed principle while using fluent)
	public class FluentBuilderInheritanceRecursiveGenerics
	{
		public FluentBuilderInheritanceRecursiveGenerics()
		{
			var me = Person.New
				.Called("Joule")
				.WorkAsA("Programer")
				.Build();
			Console.WriteLine(me);
		}
	}
}

