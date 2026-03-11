using System;

class Program
{
    static void Main()
    {
        
        Console.Write("Enter First Number: ");
        double num1 = Convert.ToDouble(Console.ReadLine());

        
        Console.Write("Enter Second Number: ");
        double num2 = Convert.ToDouble(Console.ReadLine());

        
        Console.Write("Enter Operator (+, -, *, /): ");
        string op = Console.ReadLine();

        double result = 0;
        bool valid = true;

        
        switch (op)
        {
            case "+":
                result = num1 + num2;
                break;

            case "-":
                result = num1 - num2;
                break;

            case "*":
                result = num1 * num2;
                break;

            case "/":
                if (num2 == 0)
                {
                    Console.WriteLine("Error: Division by zero is not allowed.");
                    valid = false;
                }
                else
                {
                    result = num1 / num2;
                }
                break;

            default:
                Console.WriteLine("Invalid operator! Please use +, -, *, or /.");
                valid = false;
                break;
        }

        
        if (valid)
        {
            Console.WriteLine($"Result: {result}");
        }
    }
}
