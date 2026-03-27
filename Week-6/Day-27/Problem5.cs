using System;

namespace ConsoleApp8
{
    public class ConfigurationManager
    {
        private static ConfigurationManager instance;

        public string ApplicationName { get; private set; }
        public string Version { get; private set; }
        public string DatabaseConnectionString { get; private set; }

        private ConfigurationManager()
        {
            ApplicationName = "Inventory Management System";
            Version = "1.0.0";
            DatabaseConnectionString = "Server=.;Database=InventoryDB;Trusted_Connection=True;";
        }

        public static ConfigurationManager GetInstance()
        {
            if (instance == null)
            {
                instance = new ConfigurationManager();
            }
            return instance;
        }

        public void PrintConfiguration()
        {
            Console.WriteLine("Application Name: " + ApplicationName);
            Console.WriteLine("Version: " + Version);
            Console.WriteLine("Database Connection: " + DatabaseConnectionString);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var config1 = ConfigurationManager.GetInstance();
            config1.PrintConfiguration();

            Console.WriteLine("-----------------------");

            var config2 = ConfigurationManager.GetInstance();
            config2.PrintConfiguration();

            Console.WriteLine("-----------------------");
            Console.WriteLine("Are both instances same? " + (config1 == config2));

            Console.ReadLine();
        }
    }
}