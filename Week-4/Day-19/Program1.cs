using System;

class Product
{
    // Private variables
    private int productId;
    private string productName;
    private double unitPrice;
    private int qty;

    
    public Product(int id)
    {
        productId = id;
    }

    
    public int ProductId
    {
        get { return productId; }
    }

   
    public string ProductName
    {
        get { return productName; }
        set { productName = value; }
    }

    
    public double UnitPrice
    {
        get { return unitPrice; }
        set { unitPrice = value; }
    }

    
    public int Quantity
    {
        get { return qty; }
        set { qty = value; }
    }

   
    public void ShowDetails()
    {
        double total = unitPrice * qty;

        Console.WriteLine("Product ID   : " + productId);
        Console.WriteLine("Product Name : " + productName);
        Console.WriteLine("Unit Price   : " + unitPrice);
        Console.WriteLine("Quantity     : " + qty);
        Console.WriteLine("Total Amount : " + total);
    }
}

class Program
{
    static void Main()
    {
        
        Product p1 = new Product(101);

        
        p1.ProductName = "Laptop";
        p1.UnitPrice = 50000;
        p1.Quantity = 2;

        
        p1.ShowDetails();
    }
}