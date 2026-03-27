using System;

namespace ConsoleApp8
{
    // Base Shape class
    public abstract class Shape
    {
        public abstract double GetArea();
    }

    // Rectangle class
    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public override double GetArea()
        {
            return Width * Height;
        }
    }

    // Circle class
    public class Circle : Shape
    {
        public double Radius { get; set; }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius; 
        }
    }

    // Area Calculator
    public class AreaCalculator
    {
        public void PrintArea(Shape shape)
        {
            Console.WriteLine($"The area of {shape.GetType().Name} is {shape.GetArea():F2}");
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new AreaCalculator();

            var rectObj = new Rectangle { Width = 5, Height = 10 };
            calculator.PrintArea(rectObj);

            var circleObj = new Circle { Radius = 4 };
            calculator.PrintArea(circleObj);

            Console.ReadLine();
        }
    }
}