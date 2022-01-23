using System;
using System.Text;

namespace DesignPatterns.Builder
{
	public class LifeWithoutBuilder
	{
		public LifeWithoutBuilder()
		{
			var hello = "Hello";
			var sb = new StringBuilder();
			sb.Append("<p>");
			sb.Append(hello);
			sb.Append("</p>");
			Console.WriteLine(sb);

			var words = new[] { "Hello", "World" };
			sb.Clear();
			sb.Append("<ul>");
            foreach (var word in words)
            {
				sb.AppendFormat("<li>{0}</li>", word);
            }
			sb.Append("</ul>");
			Console.WriteLine(sb);
		}
	}
}

