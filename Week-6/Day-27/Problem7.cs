using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp8
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Course { get; set; }
    }
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        List<Student> GetAllStudents();
        Student GetStudentById(int id);
        void DeleteStudent(int id);
    }
    public class StudentRepository : IStudentRepository
    {
        private List<Student> students = new List<Student>();

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public List<Student> GetAllStudents()
        {
            return students;
        }

        public Student GetStudentById(int id)
        {
            return students.FirstOrDefault(s => s.StudentId == id);
        }

        public void DeleteStudent(int id)
        {
            var student = GetStudentById(id);
            if (student != null)
            {
                students.Remove(student);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IStudentRepository repo = new StudentRepository();
            repo.AddStudent(new Student { StudentId = 1, StudentName = "Murali", Course = "Java" });
            repo.AddStudent(new Student { StudentId = 2, StudentName = "Ravi", Course = "DotNet" });
            Console.WriteLine("All Students:");
            foreach (var s in repo.GetAllStudents())
            {
                Console.WriteLine($"{s.StudentId} - {s.StudentName} - {s.Course}");
            }
            Console.WriteLine("\nSearch Student with ID = 1:");
            var student = repo.GetStudentById(1);
            if (student != null)
                Console.WriteLine($"{student.StudentId} - {student.StudentName} - {student.Course}");
            else
                Console.WriteLine("Student Not Found");
            repo.DeleteStudent(2);
            Console.WriteLine("\nAfter Deletion:");
            foreach (var s in repo.GetAllStudents())
            {
                Console.WriteLine($"{s.StudentId} - {s.StudentName} - {s.Course}");
            }
        }
    }
}