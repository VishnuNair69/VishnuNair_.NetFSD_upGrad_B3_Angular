using System;
using System.IO;

namespace ConsoleApp8
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Write("Enter root dirctory path ");
                    string root = Console.ReadLine();
                    if (!Directory.Exists(root)){
                      Console.WriteLine("Directory does not exist! Please try again.");
                      return;
                    }
                    if (string.IsNullOrWhiteSpace(root))
                    {
                        Console.WriteLine("path cannot be empty!");
                        return;
                    }
                    DirectoryInfo dio = new DirectoryInfo(root);
                    DirectoryInfo[] subdir=dio.GetDirectories();
                    if (subdir.Length == 0)
                    {
                        Console.WriteLine("No sub folders in this folder.");
                    }
                    foreach(DirectoryInfo dir in subdir)
                    {
                        FileInfo[] fi=dir.GetFiles();
                        Console.WriteLine(dir.Name);
                        Console.WriteLine(fi.Length);
                        
                    }
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