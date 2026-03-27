using System;

namespace ConsoleApp8
{
    public interface IDiscountStrategy
    {
        double CalculateDiscount(double amount);
    }

    public class RegularCustomerDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount)
        {
            return amount * 0.05;
        }
    }

    public class PremiumCustomerDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount)
        {
            return amount * 0.10;
        }
    }
    public class VipCustomerDiscount : IDiscountStrategy
    {
        public double CalculateDiscount(double amount)
        {
            return amount * 0.20;
        }
    }
    class PriceCalculator
    {
        private IDiscountStrategy discountStrategy;

        public PriceCalculator(IDiscountStrategy strategy)
        {
            discountStrategy = strategy;
        }

        public double CalculateFinalPrice(double amount)
        {
            double discount = discountStrategy.CalculateDiscount(amount);
            return amount - discount;
        }
    }



    class Program
        {
            static void Main(string[] args)
            {
                
                double orderAmount = 1000;

            PriceCalculator regularCalculator = new PriceCalculator(new RegularCustomerDiscount());
            Console.WriteLine("Regular Customer Final Price: " + regularCalculator.CalculateFinalPrice(orderAmount));

            PriceCalculator premiumCalculator = new PriceCalculator(new PremiumCustomerDiscount());
            Console.WriteLine("Premium Customer Final Price: " + premiumCalculator.CalculateFinalPrice(orderAmount));

            PriceCalculator vipCalculator = new PriceCalculator(new VipCustomerDiscount());
            Console.WriteLine("VIP Customer Final Price: " + vipCalculator.CalculateFinalPrice(orderAmount));

                Console.ReadLine();
            }
        } 
}