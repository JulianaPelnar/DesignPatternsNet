using System;
using MoreLinq;
using NUnit;
using NUnit.Framework;
// Added Testability Issues in this same File
namespace DesignPatterns.Singleton
{
	public interface IDatabase
    {
		int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        /*
         * Start of Code added for testability
         */
        private static int instanceCount; // 0
        public static int Count => instanceCount;
        /*
         * End of Code added for testability
         */


        private Dictionary<string, int> capitals;
        // constructor private to make sure there's only one instance
        private SingletonDatabase()
        {
            /*
             * Start of Code added for testability
             */
            instanceCount++;
            /*
             * End of Code added for testability
             */
        Console.WriteLine("Initializing database");
            capitals = File.ReadAllLines("Singleton/capitals.txt")
                           .Batch(2)
                           .ToDictionary(
                                list => list.ElementAt(0).Trim(),
                                list => int.Parse(list.ElementAt(1))
                            );
        }
        public int GetPopulation(string name)
        {
            return capitals[name];
        }
        // private instance and a public to be used by others
        private static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach(var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest()
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
        // The problem is the access to database, it's being hardcoded by the singleton reference.
        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] {"Seoul", "Mexico City"};
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 17400000));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
    }

    public class SingletonImplementation
	{
		public SingletonImplementation()
		{
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
		}
	}
}

