using System;
namespace DesignPatterns.Builder
{
	public enum CarType
    {
		Sedan,
		Crossover
    }
	public class Car
    {
		public CarType Type;
		public int WheelSize;
    }
	public interface ISpecifyCarType
    {
		ISpecifyWheelSize OfType(CarType type);
    }
	public interface ISpecifyWheelSize
    {
		IBuildCar WithWheels(int size);
    }
	public interface IBuildCar
    {
		public Car Build();
    }
	public class CarBuilder
    {
        // Private to keep it out of reach. May go in car if you do not mind
        // tightly coupling
        private class Impl : ISpecifyCarType, ISpecifyWheelSize, IBuildCar
        {
            private Car car = new Car();

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public IBuildCar WithWheels(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheel for {car.Type}");
                }
                car.WheelSize = size;
                return this;
            }

            public Car Build()
            {
                return car;
            }
        }

        public static ISpecifyCarType Create()
        {
            return new Impl();
        }
    }


	public class StepwiseBuilder
	{
		public StepwiseBuilder()
		{
            // Every step to invoke here will only allow one method to invoke
            // To continue the execution chain and build the object in the
            // desired order
            var car = CarBuilder.Create()  // ISpecifyCarType
                .OfType(CarType.Crossover) // ISpecifyWheelSize
                .WithWheels(18)            // IBuildCar
                .Build();
            Console.WriteLine($"Car Type: {car.Type}, Car WheelSize: {car.WheelSize}");
		}
	}
}

