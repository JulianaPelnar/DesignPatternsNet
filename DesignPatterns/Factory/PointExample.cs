﻿using System;
namespace DesignPatterns.Factory.PointExample
{
	public enum CoordinateSystem{
		Cartesian,
		Polar
    }
	public class Point
    {
		private double x, y;
		/// <summary>
        /// Initializes a point from EITHER cartesian or polar
        /// </summary>
        /// <param name="a">x if cartesian, rho if polar</param>
        /// <param name="b"></param>
        /// <param name="system"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public Point(double a, double b, CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            switch (system)
            {
				case CoordinateSystem.Cartesian:
					x = a;
					y = b;
					break;
				case CoordinateSystem.Polar:
					x = a * Math.Cos(b);
					y = a * Math.Sin(b);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }
    }
	public class PointExample
	{
		public PointExample()
		{
		}
	}
}

