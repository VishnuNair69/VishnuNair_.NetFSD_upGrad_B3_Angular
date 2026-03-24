using System;
namespace consoleApp8{
class Program
{
    static void Main()
    {
        Console.Write("Enter Product Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Product Price: ");
        double price = double.Parse(Console.ReadLine());

        Console.Write("Enter Discount Percentage: ");
        double discount = double.Parse(Console.ReadLine());

        double finalPrice = price - (price * discount / 100);
        Console.WriteLine("----- Result -----");
        Console.WriteLine("Product: " + name);
        Console.WriteLine("Original Price: " + price);
        Console.WriteLine("Discount: " + discount + "%");
        Console.WriteLine("Final Price: " + finalPrice);
    }
}
}