using System;
namespace DesignPatterns.Singleton
{
	// To have one singleton per thread. 
    // Instead of Lazy<T> which gives laziness and thread safety during initialization, a singleton per app domain.
	public sealed class PerThreadSingleton
    {
		private static ThreadLocal<PerThreadSingleton> threadInstance
			= new ThreadLocal<PerThreadSingleton>(
				() => new PerThreadSingleton());
		public int Id;
		private PerThreadSingleton()
        {
			Id = Thread.CurrentThread.ManagedThreadId;
        }
		public static PerThreadSingleton Instance => threadInstance.Value;
    }
	public class PerThreadSingletonEx
	{
		public PerThreadSingletonEx()
		{
			var t1 = Task.Factory.StartNew(() =>
			{
				Console.WriteLine("t1: " + PerThreadSingleton.Instance.Id);
			});
			var t2 = Task.Factory.StartNew(() =>
			{
				Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
				Console.WriteLine("t2: " + PerThreadSingleton.Instance.Id);
			});
			Task.WaitAll(t1, t2);
		}
	}
}

