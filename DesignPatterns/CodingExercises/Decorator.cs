using System;
namespace DesignPatterns.CodingExercises
{
    // The following code scenario shows a Dragon which is both a Bird and a Lizard
    // Complete the Dragon's interface (There's no need to extract IBird or ILizard).
    // Take special care when implementing the Age property!
    public class Bird
    {
        public int Age { get; set; }

        public string Fly()
        {
            return (Age < 10) ? "flying" : "too old";
        }
    }

    public class Lizard
    {
        public int Age { get; set; }

        public string Crawl()
        {
            return (Age > 1) ? "crawling" : "too young";
        }
    }

    public class Dragon // no need for interfaces
    {
        private Bird bird = new Bird();
        private Lizard lizard = new Lizard();
        private int age;

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                bird.Age = value;
                lizard.Age = value;
            }
        }

        public string Fly()
        {
            return bird.Fly();
        }

        public string Crawl()
        {
            return lizard.Crawl();
        }
    }
    public class Decorator
    {
        public Decorator()
        {
            var d = new Dragon();
            d.Age = 16;
            Console.WriteLine(d.Fly());
            Console.WriteLine(d.Crawl());
        }
    }
}

