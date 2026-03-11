using System;

class Program
{
    static void Main()
    {
        
        Console.Write("Enter Name: ");
        string name = Console.ReadLine();

        
        Console.Write("Enter Marks (0-100): ");
        string input = Console.ReadLine();

        
        if (int.TryParse(input, out int marks))
        {
            if (marks < 0 || marks > 100)
            {
                Console.WriteLine("Invalid marks! Please enter between 0 and 100.");
            }
            else
            {
                string grade;

                
                if (marks >= 90)
                    grade = "A";
                else if (marks >= 75)
                    grade = "B";
                else if (marks >= 50)
                    grade = "C";
                else if (marks >= 35)
                    grade = "D";
                else
                    grade = "Fail";

              
                Console.WriteLine($"Student: {name}");
                Console.WriteLine($"Grade: {grade}");
            }
        }
        else
        {
            Console.WriteLine("Invalid input! Marks must be a number.");
        }
    }
}
