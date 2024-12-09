using System.Text;

namespace DesignPatterns.CodingExercises
{
    // You're given a class called Sentence, which takes a string such as "Helllo World".
    // You need to provide an interface such that the indexer returns a WordToken which can
    // be used to capitalize a particular word in the sentence.
    // Typical use would be something like:
    // var sentence = new Sentence("hello world");
    // sentence[1].Capitalize = true;
    // WriteLine(sentence) => writes "hello WORLD"
    public class Sentence
    {
        private List<WordToken> sentenceWords = new List<WordToken>();
        public Sentence(string plainText)
        {
            // todo
            var stringArray = plainText.Split(' ');
            foreach (var word in stringArray)
            {
                sentenceWords.Add(new WordToken() { Capitalize = false, Text = word });
            }
        }

        public WordToken this[int index]
        {
            get
            {
                // todo
                return sentenceWords[index];
            }
        }

        public override string ToString()
        {
            // output formatted text here
            var sb = new StringBuilder();
            
            foreach (var word in sentenceWords)
            {
                sb.Append(word.Capitalize ? word.Text.ToUpperInvariant() : word.Text);
                if (sentenceWords.IndexOf(word) < sentenceWords.Count - 1)
                {
                    sb.Append(" ");
                }
            }

            return sb.ToString();
        }

        public class WordToken
        {
            public bool Capitalize;
            public string Text;
        }
    }
    public class FlyWeight
    {
        public FlyWeight()
        {
            var sentence = new Sentence("hello world");
            sentence[1].Capitalize = true;
            Console.WriteLine(sentence);
        }
    }
}

