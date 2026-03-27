using System;

namespace ConsoleApp8
{
    public interface IPrinter
    {
        void Print();
    }

    public interface IScanner
    {
        void Scan();
    }
    public interface IFax
      {
    void Fax();
    }
    public class BasicPrinter : IPrinter
    {
        public void Print()
        {
            Console.WriteLine("Printing document...");
        }
    }

    public class AdvancedPrinter : IPrinter, IScanner,IFax
    {
        public void Print()
        {
            Console.WriteLine("Printing document...");
        }

        public void Scan()
        {
            Console.WriteLine("Scanning document...");
        }
        public void Fax()
        {
            Console.WriteLine("faxing document");
        }
    }
    

    class Program
        {
            static void Main(string[] args)
            {

            // Usage
            var printer = new BasicPrinter();
            printer.Print();

            Console.WriteLine("---------------------------");

            var multiFunction = new AdvancedPrinter();
            multiFunction.Print();
            multiFunction.Scan();


            Console.ReadLine();
            }
        } 
}