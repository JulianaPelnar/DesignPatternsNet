using System;
namespace DesignPatterns.Factory
{
	public class Foo
    {
		private Foo() // si nobody can use it
        {
			//
        }
		private async Task<Foo> InitAsync() // only used internally
		{
			await Task.Delay(1000);
			return this;
		}
		public static Task<Foo> CreateAsync()
        {
			var result = new Foo();
			return result.InitAsync();
        }
    }
	public class AsynchronousFactoryMethod
	{
		public AsynchronousFactoryMethod() { }
		public async Task<Foo> GetCreateAsync()
		{
			var x = await Foo.CreateAsync();
			return x;
		}
	}
}

