using System;

// Step 1: Custom Exception Class
public class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message)
    {
    }
}

// Step 2: BankAccount Class
public class BankAccount
{
    private double balance;

    // Constructor
    public BankAccount(double initialBalance)
    {
        balance = initialBalance;
    }

    // Withdraw Method
    public void Withdraw(double amount)
    {
        if (amount > balance)
        {
            // Step 3: Throw custom exception
            throw new InsufficientBalanceException("Withdrawal amount exceeds available balance");
        }

        balance -= amount;
        Console.WriteLine("Withdrawal successful. Remaining Balance: " + balance);
    }
}

// Step 4: Main Program
class Program
{
    static void Main(string[] args)
    {
        // Sample Input
        BankAccount account = new BankAccount(3000);

        try
        {
            account.Withdraw(5000);
        }
        catch (InsufficientBalanceException ex)
        {
            // Step 5: Handle exception
            Console.WriteLine("Error: " + ex.Message);
        }
        finally
        {
            // Step 6: Cleanup block
            Console.WriteLine("Transaction process completed.");
        }
    }
}