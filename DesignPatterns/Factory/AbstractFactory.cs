﻿using System;
namespace DesignPatterns.Factory
{
	// abstract factory is to create abstract classes or interfaces
	public interface IHotDrink
    {
		void Consume();
    }
    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice but I'd prefer it with milk.");
        }
    }
    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is sensational!");
        }
    }
    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }
    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }
    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }
    public class HotDrinkMachine
    {
        // This code here violates the open-closed principle since, in case there's need
        // to add a new drink, there's a need to modify the code.
        // A solution is showed on AbstractFactoryOCP class.
        public enum AvailableDrink
        {
            Coffee, Tea
        }
        private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
            new Dictionary<AvailableDrink, IHotDrinkFactory>();
        public HotDrinkMachine()
        {
            foreach(AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory)Activator.CreateInstance(
                    Type.GetType("DesignPatterns.Factory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
                factories.Add(drink, factory);
            }
        }
        public IHotDrink MakeDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
    }
    public class AbstractFactory
	{
		public AbstractFactory()
		{
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            drink.Consume();
		}
	}
}

