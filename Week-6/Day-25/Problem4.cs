using System;
using System.Threading.Tasks;
namespace consoleApp8{
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Order Processing Started...\n");
        await ProcessOrderAsync();

        Console.WriteLine("\nOrder Processing Completed!");
    }

    static async Task ProcessOrderAsync()
    {
        await VerifyPaymentAsync();
        await CheckInventoryAsync();
        await ConfirmOrderAsync();
    }

    static async Task VerifyPaymentAsync()
    {
        Console.WriteLine("Verifying payment...");
        await Task.Delay(2000); 
        Console.WriteLine("Payment verified ");
    }

    static async Task CheckInventoryAsync()
    {
        Console.WriteLine("Checking inventory...");
        await Task.Delay(2000);
        Console.WriteLine("Inventory available ");
    }

    static async Task ConfirmOrderAsync()
    {
        Console.WriteLine("Confirming order...");
        await Task.Delay(2000);
        Console.WriteLine("Order confirmed ");
    }
}
}