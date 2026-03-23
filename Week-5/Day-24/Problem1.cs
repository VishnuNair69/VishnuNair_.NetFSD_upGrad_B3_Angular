using System;
using System.IO;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "TestFile.txt";

            try
            {
                while (true)
                {
                    Console.Write("Enter message (type 'exit' to stop): ");
                    string message = Console.ReadLine();

                    if (message.ToLower() == "exit")
                        break;

                    if (string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine("Message cannot be empty!");
                        continue;
                    }

                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.WriteLine($"[{DateTime.Now}] {message}");
                    }

                    Console.WriteLine("Message written successfully!\n");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(" No permission to access file.");
            }
            catch (IOException ex)
            {
                Console.WriteLine(" File error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Unexpected error: " + ex.Message);
            }

            Console.WriteLine("Program ended.");
        }
    }
}