using System;

namespace EmployeeSalaryCalculator
{
    // Base class
    class Employee
    {
        // Properties
        public string Name { get; set; }
        public double BaseSalary { get; set; }

        // Constructor
        public Employee(string name, double baseSalary)
        {
            Name = name;
            BaseSalary = baseSalary;
        }

        // Virtual method — can be overridden by derived classes
        public virtual double CalculateSalary()
        {
            return BaseSalary; // No bonus for generic employee
        }

        public virtual void DisplaySalary()
        {
            Console.WriteLine($"{Name} ({GetType().Name}) Salary = ₹{CalculateSalary()}");
        }
    }

    // Derived class — Manager (20% bonus)
    class Manager : Employee
    {
        public Manager(string name, double baseSalary)
            : base(name, baseSalary) { }

        public override double CalculateSalary()
        {
            return BaseSalary + (BaseSalary * 0.20); // 20% bonus
        }
    }

    // Derived class — Developer (10% bonus)
    class Developer : Employee
    {
        public Developer(string name, double baseSalary)
            : base(name, baseSalary) { }

        public override double CalculateSalary()
        {
            return BaseSalary + (BaseSalary * 0.10); // 10% bonus
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Employee Salary Calculator ===\n");

            // Polymorphism — base class reference holds derived class objects
            Employee[] employees = new Employee[]
            {
                new Manager("Rajesh", 50000),
                new Developer("Priya", 50000)
            };

            // Runtime polymorphism — correct CalculateSalary() called automatically
            foreach (Employee emp in employees)
            {
                emp.DisplaySalary();
            }

            Console.WriteLine();

            // Sample output format matching the requirement
            double baseSalary = 50000;
            Manager mgr = new Manager("Sample Manager", baseSalary);
            Developer dev = new Developer("Sample Developer", baseSalary);

            Console.WriteLine($"Manager Salary  = ₹{mgr.CalculateSalary()}");
            Console.WriteLine($"Developer Salary = ₹{dev.CalculateSalary()}");
        }
    }
}