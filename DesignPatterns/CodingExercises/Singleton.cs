namespace DesignPatterns.CodingExercises
{
	// Write a method called IsSingleton().
    // This method takes a factory method that returns an object and it's up
    // to you to determine wheter or not that object is a singleton instance.
	public class SingletonTester
	{
		public static bool IsSingleton(Func<object> func)
		{
			object a = func.DynamicInvoke();
			object b = func.DynamicInvoke();
			return a.Equals(b);
		}
	}
	public class Singleton
	{
		// example
		Func<object> methodCall = delegate () { Random r = new Random(); return r.Next(); };
		public Singleton()
		{
			bool a = SingletonTester.IsSingleton(methodCall);
			Console.WriteLine(a);
		}
	}
}

