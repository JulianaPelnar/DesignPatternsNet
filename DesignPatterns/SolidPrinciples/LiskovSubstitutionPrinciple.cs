using System;
namespace DesignPatterns.SolidPrinciples
{
	//Demonstration:

	public class Rectangle
    {
		public virtual int Width { get; set; }
		public virtual int Height { get; set; }

        public Rectangle()
        {

        }
        public Rectangle(int width, int height)
        {
			Width = width;
			Height = height;
		}

        public override string ToString()
        {
            return $"{nameof(Width)} : {Width}, {nameof(Height)} : {Height}";
        }
    }

	public class Square : Rectangle
    {
        public override int Width { get => base.Width; set => base.Width = base.Height = value; }
        public override int Height { get => base.Height; set => base.Height = base.Width = value; }
    }

	public class LiskovSubstitutionPrinciple
	{
        static public int Area(Rectangle r) => r.Width * r.Height;
		public LiskovSubstitutionPrinciple()
		{
            Rectangle r = new Rectangle(2, 3);
            Console.WriteLine($"{r} has area {Area(r)}");

            // This works because virtual in Rectangle and override in Square
            // Liskob principle means that it should be possible to substitute
            // a Base type for a Subtype, like Rectangle with Square
            // Otherwise, height in square would be 0
            Rectangle s = new Square();
            s.Width = 4;
            Console.WriteLine($"{s} has area {Area(s)}");
        }
	}
}

