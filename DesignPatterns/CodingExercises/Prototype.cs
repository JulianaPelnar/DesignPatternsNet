using System;
using System.Xml.Serialization;

namespace DesignPatterns.CodingExercises
{
    // Given the definitions above, you are asked to implement Line.DeepCopy()
    // to perform a deep copy of the current Line object
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
    public class Point : IPrototype<Point>
    {
		public int X, Y;
        public Point() { }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point DeepCopy()
        {
            return new Point(X, Y);
        }
    }
	public class Line : IPrototype<Line>
    {
		public Point Start, End;
        public Line() { }
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
        public Line DeepCopy()
        {
            return new Line(Start.DeepCopy(), End.DeepCopy());
        }
    }
	public class Prototype
	{
		public Prototype()
		{
            Line a = new Line(new Point(1, 2), new Point(1, 2));
            Line b = a.DeepCopy();
            Console.WriteLine(a.Start.X.ToString() + b.Start.X.ToString());
		}
	}
}
