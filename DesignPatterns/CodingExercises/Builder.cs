using System;
using System.Text;

namespace DesignPatterns.CodingExercises
{
	// You are asked to implement the Builder design pattern for rendering
	// simple chunks of code.
	// Sample use of the builder:
	// var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
	// Console.WriteLine(cb);
	// Expected output:
	// public class Person
	// {
	//   public string Name;
	//   public int Age;
	// }
	public class Code
    {
        public string Name, Type;
        public List<Code> Fields = new List<Code>();
        private const int indentSize = 2;

        public Code() { }

        public Code(string name, string type)
        {
            Name = name;
            Type = type;
        }
        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            if (indent == 0)
            {
                sb.AppendLine($"{i}public {Type} {Name}");
                sb.AppendLine("{");
            } else
            {
                sb.AppendLine($"{i}public {Type} {Name};");
            }
            foreach (var e in Fields)
                sb.Append(e.ToStringImpl(indent + 1));
            if (indent == 0)
            {
                sb.AppendLine("}");
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
	public class CodeBuilder
    {
        private readonly string className;
        Code baseClass = new Code();
        public CodeBuilder(string className)
        {
            this.className = className;
            baseClass.Name = className;
            baseClass.Type = "class";
        }
        public CodeBuilder AddField(string fieldName, string fieldType)
        {
            var c = new Code(fieldName, fieldType);
            baseClass.Fields.Add(c);
            return this;
        }
        public override string ToString()
        {
            return baseClass.ToString();
        }
        public void Clear()
        {
            baseClass = new Code { Name = className };
        }
    }
	public class Builder
	{
		public Builder()
		{
			var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
			Console.WriteLine(cb);
		}
	}
}

