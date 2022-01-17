using System;
using System.Diagnostics;

namespace DesignPatterns.SolidPrinciples
{
	// Single Responsibility:
	// Each class has one responsibility. In this case Journal only manages the entries
	// Therefore, to manage the persistance of those same entries another class is required
	// This way, in case to need modification, there are specific files or classes to affect
	// There should be only one reason to modify each class
	public class Journal
    {
		private readonly List<string> entries = new List<string>();
		private static int count = 0;

		public int AddEntry(string text)
        {
			entries.Add($"{++count}: {text}");
			return count;
        }

		public void RemoveEntry(int index)
        {
			entries.RemoveAt(index);
        }

        public override string ToString()
        {
			return string.Join(Environment.NewLine, entries);
        }
    }

	public class Persistence
    {
		public void SaveToFile(Journal j, string filename, bool overwrite = false)
        {
			if(overwrite || !File.Exists(filename))
            	File.WriteAllText(filename, j.ToString());
        }
    }

	public class SingleResponsibilityPrinciple
	{
		public SingleResponsibilityPrinciple()
		{
			var j = new Journal();
			j.AddEntry("I'm learning about SOLID principles");
			j.AddEntry("I'm thirsty");
			Console.WriteLine(j);
			// Following code throws unauthorized error so I'll block it
			/*var p = new Persistence();
			var filename = @"/Users/julianapelnar/Projects/journal.txt";
			p.SaveToFile(j, filename, true);
			Process.Start(filename);*/
		}
	}
}

