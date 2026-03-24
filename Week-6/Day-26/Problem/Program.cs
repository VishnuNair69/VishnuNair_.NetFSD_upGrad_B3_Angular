namespace ConsoleApp10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductDAL dal = new ProductDAL();
            while (true)
            {
                Console.WriteLine("\n1. Insert\n2. View\n3. Update\n4. Delete\n5. Exit");
                Console.Write("Enter choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Product p = new Product();

                        Console.Write("Enter Name: ");
                        p.ProductName = Console.ReadLine();

                        Console.Write("Enter Category: ");
                        p.Category = Console.ReadLine();

                        Console.Write("Enter Price: ");
                        p.Price = decimal.Parse(Console.ReadLine());

                        dal.InsertProduct(p);
                        break;

                    case 2:
                        dal.GetAllProducts();
                        break;

                    case 3:
                        Product up = new Product();

                        Console.Write("Enter ID: ");
                        up.ProductId = int.Parse(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        up.ProductName = Console.ReadLine();

                        Console.Write("Enter Category: ");
                        up.Category = Console.ReadLine();

                        Console.Write("Enter Price: ");
                        up.Price = decimal.Parse(Console.ReadLine());

                        dal.UpdateProduct(up);
                        break;

                    case 4:
                        Console.Write("Enter ID to delete: ");
                        int id = int.Parse(Console.ReadLine());

                        dal.DeleteProduct(id);
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}
