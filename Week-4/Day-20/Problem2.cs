using System;

namespace HRManagementSystem
{
    class Employee
    {
        // ── Private Fields — zero direct outside access ──────────────
        private string   _fullName;
        private int      _age;
        private decimal  _salary;
        private readonly string _employeeId;   // readonly — set once, never again

        private const decimal MinSalary       = 1000m;
        private const decimal MaxRaisePercent = 30m;
        private const int     MinAge          = 18;
        private const int     MaxAge          = 80;

        // ── Properties ───────────────────────────────────────────────

        public string FullName
        {
            get => _fullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Full name cannot be null, empty, or whitespace.");
                _fullName = value.Trim();      // auto-trim
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (value < MinAge || value > MaxAge)
                    throw new ArgumentException(
                        $"Age must be between {MinAge} and {MaxAge}. " +
                        $"Provided: {value}");
                _age = value;
            }
        }

        public decimal Salary
        {
            get => _salary;
            private set                        // private set — class-only mutation
            {
                if (value < MinSalary)
                    throw new ArgumentException(
                        $"Salary cannot be below company minimum of " +
                        $"₹{MinSalary}. Provided: ₹{value}");
                _salary = value;
            }
        }

        // Read-only — no setter at all outside the field assignment
        public string EmployeeId => _employeeId;

        // ── Constructors ─────────────────────────────────────────────

        // Primary constructor — explicit Employee ID
        public Employee(string fullName, decimal salary, int age, string employeeId)
        {
            // All go through property setters → validation reused automatically
            FullName = fullName;
            Age      = age;
            Salary   = salary;

            if (string.IsNullOrWhiteSpace(employeeId))
                throw new ArgumentException("Employee ID cannot be empty.");

            _employeeId = employeeId.Trim();
        }

        // Convenience constructor — auto-generates Employee ID
        public Employee(string fullName, decimal salary, int age)
            : this(fullName, salary, age, "E" + Guid.NewGuid().ToString("N")[..6].ToUpper())
        { }

        // ── Business Methods — the ONLY way to change salary ─────────

        /// <summary>
        /// Give a raise. Percentage must be > 0 and ≤ 30 (company policy).
        /// </summary>
        public void GiveRaise(decimal percentage)
        {
            if (percentage <= 0 || percentage > MaxRaisePercent)
                throw new ArgumentException(
                    $"Raise percentage must be between 0 and {MaxRaisePercent}%. " +
                    $"Provided: {percentage}%");

            decimal previousSalary = _salary;
            Salary = _salary * (1 + percentage / 100);   // goes through private setter

            Console.WriteLine($"✅ Raise applied for {FullName}: " +
                              $"₹{previousSalary:F2} → ₹{Salary:F2} " +
                              $"(+{percentage}%)");
        }

        /// <summary>
        /// Deduct a penalty. Salary must not fall below ₹1000 after deduction.
        /// Returns true if successful, false if it would violate policy.
        /// </summary>
        public bool DeductPenalty(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException(
                    $"Penalty amount must be greater than zero. Provided: ₹{amount}");

            if (_salary - amount < MinSalary)
            {
                Console.WriteLine($"❌ Penalty of ₹{amount} rejected: " +
                                  $"salary would drop below ₹{MinSalary}. " +
                                  $"Current: ₹{_salary:F2}");
                return false;
            }

            decimal previousSalary = _salary;
            Salary = _salary - amount;

            Console.WriteLine($"⚠️  Penalty deducted for {FullName}: " +
                              $"₹{previousSalary:F2} → ₹{Salary:F2} (-₹{amount})");
            return true;
        }

        // ── Display ──────────────────────────────────────────────────

        public void DisplayProfile()
        {
            Console.WriteLine(new string('─', 42));
            Console.WriteLine($"  Employee ID : {EmployeeId}");
            Console.WriteLine($"  Name        : {FullName}");
            Console.WriteLine($"  Age         : {Age}");
            Console.WriteLine($"  Salary      : ₹{Salary:F2}");
            Console.WriteLine(new string('─', 42));
        }
    }

    // ── Program Entry ─────────────────────────────────────────────────
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║      HR Employee Management System       ║");
            Console.WriteLine("╚══════════════════════════════════════════╝\n");

            // ── Valid employee creation ──────────────────────────────
            Console.WriteLine("▶ Creating employees...\n");

            var emp1 = new Employee("Vishnu Sharma", 4500m, 25, "E1001");
            var emp2 = new Employee("Priya Nair",    3200m, 32);   // auto ID

            emp1.DisplayProfile();
            emp2.DisplayProfile();

            // ── GiveRaise ────────────────────────────────────────────
            Console.WriteLine("\n▶ Salary Operations\n");

            emp1.GiveRaise(15);           // ✅ 4500 → 5175
            emp2.GiveRaise(10);           // ✅ 3200 → 3520

            // ── DeductPenalty ────────────────────────────────────────
            emp1.DeductPenalty(175);      // ✅ 5175 → 5000
            emp2.DeductPenalty(3000);     // ❌ would breach ₹1000 minimum

            // ── Updated profiles ─────────────────────────────────────
            Console.WriteLine("\n▶ Updated Profiles\n");
            emp1.DisplayProfile();
            emp2.DisplayProfile();

            // ── Forbidden usage demos (exception catching) ───────────
            Console.WriteLine("\n▶ Validation Guard Tests\n");

            // emp1.Salary = 800;        // ← won't even compile (private set)
            // emp1._salary = -10000;    // ← won't compile (private field)

            RunTest("Empty name",
                () => emp1.FullName = "");

            RunTest("Whitespace name",
                () => emp1.FullName = "     ");

            RunTest("Age too low (12)",
                () => emp1.Age = 12);

            RunTest("Age too high (90)",
                () => emp1.Age = 90);

            RunTest("Raise too high (40%)",
                () => emp1.GiveRaise(40));

            RunTest("Raise negative (-5%)",
                () => emp1.GiveRaise(-5));

            RunTest("Create with salary below ₹1000",
                () => new Employee("Ghost User", 500m, 30));

            RunTest("Create with age = 15",
                () => new Employee("Teen Employee", 2000m, 15));
        }

        // Helper — runs a test, catches and prints the exception neatly
        static void RunTest(string label, Action action)
        {
            try
            {
                action();
                Console.WriteLine($"  [{label}] — No exception thrown ⚠️");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"  ❌ [{label}]\n     → {ex.Message}");
            }
        }
    }
}