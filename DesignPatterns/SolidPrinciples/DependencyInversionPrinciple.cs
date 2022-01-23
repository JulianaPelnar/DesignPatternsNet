using System;
namespace DesignPatterns.SolidPrinciples
{
	// High level parts of the system should not depend on
    // low level parts of the system but on abstraction

	public enum Relationship
    {
		Parent, Child, Sibling
    }

	public class Person
    {
		public string Name;
    }

	public interface IRelationshipBrowser
    {
		IEnumerable<Person> FindAllChildrenOf(string name);
    }

	// low level
	public class Relationships : IRelationshipBrowser
    {
		private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

		public void AddParentAndChild(Person parent, Person child)
        {
			relations.Add((parent, Relationship.Parent, child));
			relations.Add((child, Relationship.Child, parent));
		}

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
			foreach (var r in relations.Where(x => x.Item1.Name == name
												   && x.Item2 == Relationship.Parent))
			{
				yield return r.Item3;
			}
		}
		// Wrong
        //public List<(Person, Relationship, Person)> Relations => relations;

    }

	public class Research
    {
        // Worng
        /*public Research(Relationships relationships)
        {
			var relations = relationships.Relation;
			foreach (var r in relations.Where(x => x.Item1.Name == "John"
												   && x.Item2 == Relationship.Parent))
			{
				Console.WriteLine($"John has a child named {r.Item3.Name}");
			}
		}*/
        // New
        public Research(IRelationshipBrowser browser)
        {
            foreach (Person? p in browser.FindAllChildrenOf("John"))
            {
				Console.WriteLine($"John has a child called {p.Name}");
            }
        }
    }

	public class DependencyInversionPrinciple
	{
        public DependencyInversionPrinciple()
        {
			var parent = new Person { Name = "John" };
			var child1 = new Person { Name = "Chris" };
			var child2 = new Person { Name = "Mary" };

			var relationships = new Relationships();
			relationships.AddParentAndChild(parent, child1);
			relationships.AddParentAndChild(parent, child2);

			new Research(relationships);
		}
	}
}

