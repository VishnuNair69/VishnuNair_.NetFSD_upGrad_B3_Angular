using System;
using System.Threading;
using System.Threading.Tasks;
namespace  consoleApp8{
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting report generation...\n");

        Task t1 = Task.Run(() => GenerateSalesReport());
        Task t2 = Task.Run(() => GenerateInventoryReport());
        Task t3 = Task.Run(() => GenerateCustomerReport());
        await Task.WhenAll(t1, t2, t3);
        Console.WriteLine("\nAll reports generated successfully!");
    }

    static void GenerateSalesReport()
    {
        Console.WriteLine("Sales Report Started...");
        Thread.Sleep(2000);
        Console.WriteLine("Sales Report Completed!");
    }

    static void GenerateInventoryReport()
    {
        Console.WriteLine("Inventory Report Started...");
        Thread.Sleep(3000);
        Console.WriteLine("Inventory Report Completed!");
    }

    static void GenerateCustomerReport()
    {
        Console.WriteLine("Customer Report Started...");
        Thread.Sleep(2500);
        Console.WriteLine("Customer Report Completed!");
    }
}
}