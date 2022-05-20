using System;
namespace DesignPatterns.Factory.InnerFactory
{
	public class Point
    {
		private double x, y;
		internal Point(double x, double y)
        {
			this.x = x;
			this.y = y;
        }
        public override string ToString()
        {
			return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
		public static Point Origin => new Point(0, 0);
		// the => indicates property and initializes each time you access the property. Good when you need a new object each time
		public static Point Origin2 = new Point(0, 0); // better in this case because it's only initialized once and available always
		public static class Factory
		{
			public static Point NewCartesianPoint(double x, double y)
			{
				return new Point(x, y);
			}
			public static Point NewPolarPoint(double rho, double theta)
			{
				return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
			}
		}
	}
	public class InnerFactory
	{
		public InnerFactory()
		{
			var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
			Console.WriteLine(point);
		}
	}
}

