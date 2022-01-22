using System;
namespace DesignPatterns.SolidPrinciples
{
	// Class should be open to EXTENSION yet closed to MODIFICATION

	// Implement enterprise pattern called Specification Pattern
	// to avoid modification of class each time a filter is added


	public enum Color
    {
		Red, Green, Blue
    }

	public enum Size
    {
		Small, Medium, Large
    }

	public class Product
    {
		public String Name { get; set; }
		public Color Color { get; set; }
		public Size Size { get; set; }

        public Product(string name, Color color, Size size)
        {
			Name = name;
			Color = color;
			Size = size;
        }
    }

	// Wrong
	public class ProductFilter
    {
		public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
				if (p.Size == size)
					yield return p;
        }

		public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
				if (p.Color == color)
					yield return p;
        }

		public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
				if (p.Size == size && p.Color == color)
					yield return p;
        }
    }

    // Correct

    public interface ISpecification<T>
    {
		bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
		IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

	public class ColorSpecification: ISpecification<Product>
    {
		private Color Color;
        public ColorSpecification(Color color)
        {
			Color = color;
        }
		public bool IsSatisfied(Product p)
        {
			return p.Color == Color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size Size;
        public SizeSpecification(Size size)
        {
            Size = size;
        }
        public bool IsSatisfied(Product p)
        {
            return p.Size == Size;
        }
    }


    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;
        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class FilterProducts : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    public class Open_ClosedPrinciple
	{
		public Open_ClosedPrinciple()
		{
			var apple = new Product("Apple", Color.Green, Size.Small);
			var tree = new Product("Tree", Color.Green, Size.Large);
			var house = new Product("House", Color.Blue, Size.Large);

			Product[] products = { apple, tree, house };
			var pf = new ProductFilter();
			Console.WriteLine("Green products (old): ");
            foreach (var p in pf.FilterByColor(products, Color.Green))
            {
				Console.WriteLine($" - {p.Name} is green");
            }

            var f = new FilterProducts();
            Console.WriteLine("Green products (new): ");
            foreach (var p in f.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {p.Name} is green");
            }
            Console.WriteLine("Large blue items: ");
            foreach (var p in f.Filter(products, new AndSpecification<Product>(new ColorSpecification(Color.Blue),
                                                                               new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($" - {p.Name} is big and blue");
            }
        }
	}
}

