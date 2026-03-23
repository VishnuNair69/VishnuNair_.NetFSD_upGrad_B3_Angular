using System;

class EmployeePerformance
{
    static void Main()
    {
        try
        {
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Monthly Sales Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal sales))
            {
                Console.WriteLine("Invalid sales amount!");
                return;
            }
            Console.Write("Enter Customer Feedback Rating (1-5): ");
            int rating=Convert.ToInt32(Console.ReadLine());
            if  (rating < 1 || rating > 5)
            {
                Console.WriteLine("Invalid rating! Must be 1 to 5.");
                return;
            }
            var performanceData = GetPerformanceData(sales, rating);
            string category = performanceData switch
            {
                (>= 100000, >= 4) => "High Performer",
                (>= 50000, >= 3) => "Average Performer",
                _ => "Needs Improvement"
            };

            Console.WriteLine($"Employee Name: {name}");
            Console.WriteLine($"Sales Amount: {performanceData.sales}");
            Console.WriteLine($"Rating: {performanceData.rating}");
            Console.WriteLine($"Performance: {category}");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }
    }

    static (decimal sales, int rating) GetPerformanceData(decimal sales, int rating)
    {
        return (sales, rating);
    }
}