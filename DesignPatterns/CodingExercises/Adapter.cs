using System;
namespace DesignPatterns.CodingExercises
{
    // You're given an IRectangle interface and an extension method on it.
    // Try to define a SquareToRectangleAdapter that adapts the Square to the
    // IRectangle interface.
    public class Square
    {
        public int Side;
    }

    public interface IRectangle
    {
        int Width { get; }
        int Height { get; }
    }

    public static class ExtensionMethods
    {
        public static int Area(this IRectangle rc)
        {
            return rc.Width * rc.Height;
        }
    }

    public class SquareToRectangleAdapter : IRectangle
    {
        private int side;
        public SquareToRectangleAdapter(Square square)
        {
            side = square.Side;
        }

        public int Width => side;

        public int Height => side;
    }
    public class Adapter
    {
        public Adapter()
        {
        }
    }
}

