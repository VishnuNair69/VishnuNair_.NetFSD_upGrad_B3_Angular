using System;

class Calculator
{
    // Method for Addition
    public int Add(int a, int b)
    {
        return a + b;
    }

    // Method for Subtraction
    public int Subtract(int a, int b)
    {
        return a - b;
    }
}

class Program
{
    static void Main()
    {
        int num1, num2;

        Console.Write("Enter first number: ");
        num1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter second number: ");
        num2 = Convert.ToInt32(Console.ReadLine());

        // Creating object of Calculator class
        Calculator calc = new Calculator();

        // Calling methods
        int addition = calc.Add(num1, num2);
        int subtraction = calc.Subtract(num1, num2);

        // Displaying output
        Console.WriteLine("Addition = " + addition);
        Console.WriteLine("Subtraction = " + subtraction);
    }
}