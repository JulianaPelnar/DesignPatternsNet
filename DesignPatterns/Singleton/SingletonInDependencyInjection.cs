using System;
using Autofac;
using MoreLinq;
using NUnit.Framework;

namespace DesignPatterns.Singleton.DI
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }
    public class SingletonDatabase : IDatabase
    {
        private static int instanceCount; // 0
        public static int Count => instanceCount;

        private Dictionary<string, int> capitals;
        private SingletonDatabase()
        {
            instanceCount++;
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
        private static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;
    }
    public class OrdinaryDatabase // not a singleton but used as one by dependency injection
    {
        private Dictionary<string, int> capitals;
        private OrdinaryDatabase()
        {
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
    }
    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += SingletonDatabase.Instance.GetPopulation(name);
            return result;
        }
    }
    public class ConfigurableRecordFinder
    {
        private IDatabase database;
        public ConfigurableRecordFinder(IDatabase database)
        {
                this.database = database ?? throw new ArgumentNullException(paramName: nameof(database));
        }
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
                result += database.GetPopulation(name);
            return result;
        }
    }
    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
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
        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] { "Seoul", "Mexico City" };
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(17500000 + 17400000));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
        [Test]
        public void ConfigurablePopulationTest()
        {
            var crf = new ConfigurableRecordFinder(new DummyDatabase());
            var names = new[] { "alpha", "gamma" };
            int tp = crf.GetTotalPopulation(names);
            Assert.That(tp, Is.EqualTo(4));
        }
        [Test]
        public void DIPopulationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<DummyDatabase>()
                .As<IDatabase>()
                .SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();
            using (var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
                var names = new[] { "alpha", "gamma" };
                int tp = rf.GetTotalPopulation(names);
                Assert.That(tp, Is.EqualTo(4));
            }
        }
    }
    public class SingletonInDependencyInjection
	{
		public SingletonInDependencyInjection()
        {
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
        }
	}
}
