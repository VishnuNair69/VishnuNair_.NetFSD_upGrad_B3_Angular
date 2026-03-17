using System;

namespace BankAccountSystem
{
    class BankAccount
    {
        // Private fields — data hiding
        private string accountNumber;
        private double balance;

        // Constructor
        public BankAccount(string accNumber, double initialBalance = 0)
        {
            accountNumber = accNumber;
            balance = initialBalance >= 0 ? initialBalance : 0;
        }

        // Read-only property for Account Number
        public string AccountNumber
        {
            get { return accountNumber; }
        }

        // Read-only property for Balance (no public setter)
        public double Balance
        {
            get { return balance; }
        }

        // Deposit Method
        public void Deposit(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("❌ Deposit failed: Amount must be greater than zero.");
                return;
            }
            balance += amount;
            Console.WriteLine($"✅ Deposited: ₹{amount}");
            Console.WriteLine($"💰 Current Balance = ₹{balance}");
        }

        // Withdraw Method
        public void Withdraw(double amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine("❌ Withdrawal failed: Amount must be greater than zero.");
                return;
            }
            if (amount > balance)
            {
                Console.WriteLine("❌ Withdrawal failed: Insufficient balance.");
                Console.WriteLine($"💰 Current Balance = ₹{balance}");
                return;
            }
            balance -= amount;
            Console.WriteLine($"✅ Withdrawn: ₹{amount}");
            Console.WriteLine($"💰 Current Balance = ₹{balance}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Bank Account Management System ===\n");

            BankAccount account = new BankAccount("ACC001");

            // Sample Input: Deposit 5000, Withdraw 2000
            account.Deposit(5000);
            Console.WriteLine();
            account.Withdraw(2000);
            Console.WriteLine();

            // Extra validation test cases
            Console.WriteLine("--- Testing edge cases ---");
            account.Deposit(-100);    // Should fail
            account.Withdraw(9999);   // Should fail — insufficient funds
            account.Withdraw(0);      // Should fail — zero amount
        }
    }
}