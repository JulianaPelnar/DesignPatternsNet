using System;
namespace DesignPatterns.Singleton
{
	public class CEO
    {
		private static string name;
		private static int age;
		public string Name
        {
			get => name;
			set => name = value;
        }
		public int Age
		{
			get => age;
			set => age = value;
		}
        public override string ToString()
        {
			return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }
	public class Monostate
	{
		public Monostate()
		{
			var ceo = new CEO();
			ceo.Name = "Adam Smith";
			ceo.Age = 55;
			var ceo2 = new CEO();
			Console.WriteLine(ceo2);
			ceo2.Age = 45;
			Console.WriteLine(ceo);
			// since the Name and Age refer to static fields, they'll always share data even though value can be changed
		}
	}
}

