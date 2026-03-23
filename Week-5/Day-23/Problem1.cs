using System;
using System.Collections.Generic;
using System.Collections.Immutable;
namespace ConsoleApp8
{ 
class Program
{        
    static List<String> tasks=new List<String>();

  static void Main(String[] args)
{
    while(true)
    {
   Console.WriteLine();
   Console.WriteLine("To-Do List Manager");
   Console.WriteLine("1. Add Task");
   Console.WriteLine("2. View Tasks");
   Console.WriteLine("3. Remove Task");
   Console.WriteLine("4. Exit");
   Console.Write("enter your option:");
   int option=Convert.ToInt32(Console.ReadLine());
   Console.WriteLine();
                switch (option)
                {
                    case 1: AddTask();
                            break;
                    case 2: ViewTask();
                            break;
                    case 3: RemoveTask();
                            break;
                    case 4: return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
    } }
        static void AddTask()
            {
                Console.Write("enter task:");
                String task=Console.ReadLine();
                if(String.IsNullOrWhiteSpace(task)){
                Console.WriteLine("task cannot be empty");
            }
            else
            {
                tasks.Add(task);
                Console.WriteLine("task added!");
                
            }
            } 
        static void ViewTask()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("tasks are empty");
            }
            else
            {
                for(int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i+1}.{tasks[i]}");
                }
            }
        }
        static void RemoveTask()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("tasks are empty");
            }
            else
            {
                Console.WriteLine("enter task to remove");
                int num=Convert.ToInt32(Console.ReadLine());
                if (num>0&&num<=tasks.Count)
                {
                    tasks.RemoveAt(num-1);
                    Console.WriteLine("task removed sucessfully");
                }
                else
                {
                    Console.WriteLine("invalid task");
                }
            }
        }
}
}