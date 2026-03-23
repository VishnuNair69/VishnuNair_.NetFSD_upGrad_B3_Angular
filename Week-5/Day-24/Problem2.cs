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
                    Console.Write("Enter dirctory path ");
                    string path = Console.ReadLine();
                    if (!Directory.Exists(path)){
                      Console.WriteLine("Directory does not exist! Please try again.");
                    continue;
                    }
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        Console.WriteLine("path cannot be empty!");
                        continue;
                    }
                    DirectoryInfo dio = new DirectoryInfo(path);
                    FileInfo[] files=dio.GetFiles();
                    if (files.Length == 0)
                    {
                        Console.WriteLine("No files found in this folder.");
                    }
                    foreach(FileInfo fi in files)
                    {
                        Console.WriteLine(fi.Name);
                        Console.WriteLine(fi.Length);
                        Console.WriteLine(fi.CreationTime);
                    }
                  Console.WriteLine("totalfiles"+files.Length);
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