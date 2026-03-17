using System;

record Student(int RollNo, string Name, string Course, int Marks);

class Program
{
    static void Main(string[] args)
    {
        // Read array size from user
        Console.Write("Enter number of students: ");
        int n = int.Parse(Console.ReadLine());

        // Create array of Student records
        Student[] students = new Student[n];

        // Input all student details
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine("\nEnter details for Student " + (i + 1));

            Console.Write("Roll Number: ");
            int roll = int.Parse(Console.ReadLine());

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Course: ");
            string course = Console.ReadLine();

            Console.Write("Marks: ");
            int marks = int.Parse(Console.ReadLine());

            students[i] = new Student(roll, name, course, marks);
            Console.WriteLine("Student added!");
        }

        // Display all records
        Console.WriteLine("\n--- All Student Records ---");
        for (int i = 0; i < students.Length; i++)
        {
            Console.WriteLine("Roll No: " + students[i].RollNo +
                              " | Name: " + students[i].Name +
                              " | Course: " + students[i].Course +
                              " | Marks: " + students[i].Marks);
        }

        // Search by Roll Number
        Console.Write("\nEnter Roll Number to search: ");
        int searchRoll = int.Parse(Console.ReadLine());

        bool found = false;

        for (int i = 0; i < students.Length; i++)
        {
            if (students[i].RollNo == searchRoll)
            {
                Console.WriteLine("Student Found!");
                Console.WriteLine("Roll No: " + students[i].RollNo +
                                  " | Name: " + students[i].Name +
                                  " | Course: " + students[i].Course +
                                  " | Marks: " + students[i].Marks);
                found = true;
                break;
            }
        }

        if (found == false)
        {
            Console.WriteLine("Student not found.");
        }
    }
}
