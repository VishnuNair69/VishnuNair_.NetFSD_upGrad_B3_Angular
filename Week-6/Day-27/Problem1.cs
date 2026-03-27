using System;
using System.Collections.Generic;

namespace StudentApp
{
    class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public double Marks { get; set; }
    }

    class StudentManager
    {
        private List<Student> students = new List<Student>();

        public void AddStudent(Student student)
        {
            if (student.StudentId <= 0)
                throw new ArgumentException("Invalid Student ID");

            if (string.IsNullOrWhiteSpace(student.StudentName))
                throw new ArgumentException("Student Name cannot be empty");

            if (student.Marks < 0 || student.Marks > 100)
                throw new ArgumentException("Marks must be between 0 and 100");

            if (students.Exists(s => s.StudentId == student.StudentId))
                throw new Exception("Duplicate Student ID not allowed");

            students.Add(student);
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }
    }

    class ReportGenerator
    {
        public void GenerateReport(List<Student> students)
        {
            Console.WriteLine("\n----- Student Report -----");

            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.StudentId}, Name: {student.StudentName}, Marks: {student.Marks}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            StudentManager manager = new StudentManager();
            ReportGenerator report = new ReportGenerator();

            try
            {
                manager.AddStudent(new Student { StudentId = 1, StudentName = "Vishnu", Marks = 85 });
                manager.AddStudent(new Student { StudentId = 2, StudentName = "Ravi", Marks = 90 });
                //manager.AddStudent(new Student { StudentId = -1, StudentName = "", Marks = 120 });
                List<Student> students = manager.GetAllStudents();
                report.GenerateReport(students);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadLine();
        }
    }
}