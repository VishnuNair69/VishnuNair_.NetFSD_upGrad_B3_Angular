using System;
using System.Collections.Generic;

namespace OnlineShoppingCart
{
    
    class Product
    {
        private string name;
        private double price;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Console.WriteLine("❌ Name cannot be empty.");
                else
                    name = value;
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    Console.WriteLine("❌ Price cannot be negative.");
                else
                    price = value;
            }
        }

        public Product(string name, double price)
        {
            Name  = name;
            Price = price;
        }

        // Virtual — derived classes override with their own discount logic
        public virtual double CalculateDiscount()
        {
            return 0; // No discount for generic product
        }

        public double FinalPrice()
        {
            return Price - CalculateDiscount();
        }

        public virtual void DisplayDetails()
        {
            Console.WriteLine($"  Product  : {Name}");
            Console.WriteLine($"  Category : {GetType().Name}");
            Console.WriteLine($"  MRP      : ₹{Price}");
            Console.WriteLine($"  Discount : ₹{CalculateDiscount()} " +
                              $"({GetDiscountPercent()}% off)");
            Console.WriteLine($"  Final    : ₹{FinalPrice()}");
            Console.WriteLine(new string('─', 38));
        }

        protected virtual double GetDiscountPercent() => 0;
    }

    // ─── Derived Class: Electronics (5% discount) ─────────────────
    class Electronics : Product
    {
        private const double DiscountRate = 0.05;

        public Electronics(string name, double price)
            : base(name, price) { }

        public override double CalculateDiscount()
        {
            return Price * DiscountRate;
        }

        protected override double GetDiscountPercent() => DiscountRate * 100;
    }

    // ─── Derived Class: Clothing (15% discount) ───────────────────
    class Clothing : Product
    {
        private const double DiscountRate = 0.15;

        public Clothing(string name, double price)
            : base(name, price) { }

        public override double CalculateDiscount()
        {
            return Price * DiscountRate;
        }

        protected override double GetDiscountPercent() => DiscountRate * 100;
    }

    // ─── Shopping Cart ────────────────────────────────────────────
    class ShoppingCart
    {
        private List<Product> items = new List<Product>();

        public void AddItem(Product product)
        {
            items.Add(product);
            Console.WriteLine($"✅ Added '{product.Name}' to cart.\n");
        }

        public void DisplayCart()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("🛒 Cart is empty.");
                return;
            }

            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║        YOUR SHOPPING CART            ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            double cartTotal    = 0;
            double totalSavings = 0;

            foreach (Product item in items)   // Polymorphism in action
            {
                item.DisplayDetails();
                cartTotal    += item.FinalPrice();
                totalSavings += item.CalculateDiscount();
            }

            Console.WriteLine($"\n  🛍️  Items       : {items.Count}");
            Console.WriteLine($"  💸  You Saved   : ₹{totalSavings}");
            Console.WriteLine($"  💰  Cart Total  : ₹{cartTotal}");
            Console.WriteLine(new string('═', 38));
        }
    }

    // ─── Entry Point ──────────────────────────────────────────────
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Online Shopping Cart System ===\n");

            // --- Sample output matching the requirement ---
            Electronics laptop = new Electronics("Laptop", 20000);
            double discount    = laptop.CalculateDiscount();
            double finalPrice  = laptop.FinalPrice();
            Console.WriteLine($"Final Price after 5% discount = ₹{finalPrice}\n");
            Console.WriteLine(new string('─', 38) + "\n");

            // --- Full cart demo ---
            ShoppingCart cart = new ShoppingCart();
            cart.AddItem(new Electronics("Laptop",       20000));
            cart.AddItem(new Electronics("Smartphone",   15000));
            cart.AddItem(new Clothing("Formal Shirt",     1200));
            cart.AddItem(new Clothing("Denim Jeans",      2500));

            cart.DisplayCart();

            // --- Validation tests ---
            Console.WriteLine("\n--- Validation Tests ---");
            new Electronics("Tablet", -500);   // Negative price blocked
            new Clothing("",          800);    // Empty name blocked
        }
    }
}
