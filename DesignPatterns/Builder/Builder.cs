using System;
using System.Text;

namespace DesignPatterns.Builder
{
	public class HtmlElement
    {
		public string Name, Text;
		public List<HtmlElement> Elements = new List<HtmlElement>();
		private const int indentSize = 4;

        public HtmlElement(){}

        public HtmlElement(string name, string text)
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
			foreach(var e in Elements)
				sb.Append(e.ToStringImpl(indent + 1));
			sb.AppendLine($"{i}<{Name}>");
			return sb.ToString();
		}
        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
	public class HtmlBuilder
    {
		private readonly string rootName;
		HtmlElement root = new HtmlElement();
		public HtmlBuilder(string rootName)
        {
			this.rootName = rootName;
			root.Name = rootName;
        }
		public void AddChild(string childName, string childText)
        {
			var e = new HtmlElement(childName, childText);
			root.Elements.Add(e);
        }
        public override string ToString()
        {
            return root.ToString();
        }
		public void Clear()
        {
			root = new HtmlElement { Name = rootName };
        }
    }
	public class Builder
	{
		public Builder()
		{
			var builder = new HtmlBuilder("ul");
			builder.AddChild("li", "Hello");
			builder.AddChild("li", "World");
			Console.WriteLine(builder.ToString());
		}
	}
}

