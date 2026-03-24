using System;
using System.Threading.Tasks;
namespace consoleApp8{
class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Application Started...");
        Task t1 = WriteLogAsync("User logged in");
        await t1;
        Console.WriteLine(" logs completed!");
    }
    public static async Task WriteLogAsync(string message)
    {
        Console.WriteLine($"Start: {message}");
        await Task.Delay(2000);
        Console.WriteLine($"End: {message}");
    }
}
}