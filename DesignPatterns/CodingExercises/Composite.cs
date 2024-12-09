using System;
using System.Collections;

namespace DesignPatterns.CodingExercises.Composite
{
    // Consider the code presented below. The Sum() extension method adds up all the values in a list of IValueContainer element it gets
    // passed. We can have a single value or a set of values.
    // Complete the implementation of the interfaces so that Sum() begins to work correctly.
    public interface IValueContainer : IEnumerable<int>
    {
    }

    public class SingleValue : IValueContainer
    {
        public int Value;

        public IEnumerator<int> GetEnumerator()
        {
            yield return Value;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }

    public class ManyValues : List<int>, IValueContainer
    {
    }

    public static class ExtensionMethods
    {
        public static int Sum(this List<IValueContainer> containers)
        {
            int result = 0;
            foreach (var c in containers)
                foreach (var i in c)
                    result += i;
            return result;
        }
    }
    public class Composite
    {
        public Composite()
        {
        }
    }
}

