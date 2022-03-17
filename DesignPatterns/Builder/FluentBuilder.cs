using System;
using System.Text;

namespace DesignPatterns.Builder
{
    public class HtmlElementFluent
    {
        public string Name, Text;
        public List<HtmlElementFluent> Elements = new List<HtmlElementFluent>();
        private const int indentSize = 4;

        public HtmlElementFluent() { }

        public HtmlElementFluent(string name, string text)
        {
            Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }
        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }
            foreach (var e in Elements)
                sb.Append(e.ToStringImpl(indent + 1));
            sb.AppendLine($"{i}<{Name}>");
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
    public class HtmlBuilderFluent
    {
        private readonly string rootName;
        HtmlElementFluent root = new HtmlElementFluent();
        public HtmlBuilderFluent(string rootName)
        {
            this.rootName = rootName;
            root.Name = rootName;
        }
        public HtmlBuilderFluent AddChild(string childName, string childText)
        {
            var e = new HtmlElementFluent(childName, childText);
            root.Elements.Add(e);
            return this; // ---> Only THIS change needed to make it fluent
        }
        public override string ToString()
        {
            return root.ToString();
        }
        public void Clear()
        {
            root = new HtmlElementFluent { Name = rootName };
        }
    }
    public class FluentBuilder
    {
        public FluentBuilder()
        {
            var builder = new HtmlBuilderFluent("ul");
            builder.AddChild("li", "Hello").AddChild("li", "World"); // ---> Now you can chain the AddChild
            // Chain calls, fluent interface to allows you to chain several calls by returning
            // a reference to the object you're working with
            Console.WriteLine(builder.ToString());
        }
    }
}

