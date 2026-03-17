using System;
using System.Collections.Generic;

namespace VehicleRentalSystem
{
    // ─── Base Class ───────────────────────────────────────────────
    class Vehicle
    {
        private string brand;
        private double rentalRatePerDay;

        public string Brand
        {
            get { return brand; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Console.WriteLine("❌ Brand name cannot be empty.");
                else
                    brand = value;
            }
        }

        public double RentalRatePerDay
        {
            get { return rentalRatePerDay; }
            set
            {
                if (value <= 0)
                    Console.WriteLine("❌ Rental rate must be greater than zero.");
                else
                    rentalRatePerDay = value;
            }
        }

        public Vehicle(string brand, double rentalRatePerDay)
        {
            Brand            = brand;
            RentalRatePerDay = rentalRatePerDay;
        }

        // Virtual method — derived classes override with their own logic
        public virtual double CalculateRental(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("❌ Rental days must be at least 1.");
                return 0;
            }
            return RentalRatePerDay * days;
        }

        protected virtual string GetChargeBreakdown(int days) =>
            $"  Base Charge : ₹{RentalRatePerDay} x {days} days = ₹{RentalRatePerDay * days}";

        public void DisplayRentalSummary(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("❌ Rental days must be at least 1.");
                return;
            }

            double total = CalculateRental(days);

            Console.WriteLine($"  Brand       : {Brand}");
            Console.WriteLine($"  Type        : {GetType().Name}");
            Console.WriteLine($"  Rate/Day    : ₹{RentalRatePerDay}");
            Console.WriteLine($"  Days        : {days}");
            Console.WriteLine(GetChargeBreakdown(days));
            Console.WriteLine($"  Total       : ₹{total}");
            Console.WriteLine(new string('─', 40));
        }
    }

    // ─── Derived Class: Car (+₹500 insurance per rental) ──────────
    class Car : Vehicle
    {
        private const double InsuranceCharge = 500;

        public Car(string brand, double rentalRatePerDay)
            : base(brand, rentalRatePerDay) { }

        public override double CalculateRental(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("❌ Rental days must be at least 1.");
                return 0;
            }
            double baseCharge = RentalRatePerDay * days;
            return baseCharge + InsuranceCharge;        // Flat ₹500 insurance added
        }

        protected override string GetChargeBreakdown(int days)
        {
            double baseCharge = RentalRatePerDay * days;
            return $"  Base Charge : ₹{RentalRatePerDay} x {days} days = ₹{baseCharge}\n" +
                   $"  Insurance   : +₹{InsuranceCharge} (flat)";
        }
    }

    // ─── Derived Class: Bike (5% discount on total) ───────────────
    class Bike : Vehicle
    {
        private const double DiscountRate = 0.05;

        public Bike(string brand, double rentalRatePerDay)
            : base(brand, rentalRatePerDay) { }

        public override double CalculateRental(int days)
        {
            if (days <= 0)
            {
                Console.WriteLine("❌ Rental days must be at least 1.");
                return 0;
            }
            double baseCharge = RentalRatePerDay * days;
            double discount   = baseCharge * DiscountRate;
            return baseCharge - discount;               // 5% off total
        }

        protected override string GetChargeBreakdown(int days)
        {
            double baseCharge = RentalRatePerDay * days;
            double discount   = baseCharge * DiscountRate;
            return $"  Base Charge : ₹{RentalRatePerDay} x {days} days = ₹{baseCharge}\n" +
                   $"  Discount    : -₹{discount} (5% off)";
        }
    }

    // ─── Rental Agency (manages fleet polymorphically) ────────────
    class RentalAgency
    {
        private List<Vehicle> fleet = new List<Vehicle>();

        public void AddVehicle(Vehicle vehicle)
        {
            fleet.Add(vehicle);
        }

        public void DisplayAllRentals(int days)
        {
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║         VEHICLE RENTAL SUMMARY           ║");
            Console.WriteLine("╚══════════════════════════════════════════╝\n");

            double grandTotal = 0;

            foreach (Vehicle v in fleet)        // Runtime polymorphism — correct
            {                                   // CalculateRental() called per type
                v.DisplayRentalSummary(days);
                grandTotal += v.CalculateRental(days);
            }

            Console.WriteLine($"\n  🚗  Vehicles   : {fleet.Count}");
            Console.WriteLine($"  📅  Days       : {days}");
            Console.WriteLine($"  💰  Grand Total: ₹{grandTotal}");
            Console.WriteLine(new string('═', 40));
        }
    }

    // ─── Entry Point ──────────────────────────────────────────────
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Vehicle Rental System ===\n");

            // --- Sample output matching the requirement ---
            // Car: 2000 x 3 = 6000 + 500 insurance = 6500
            Vehicle sampleCar = new Car("Toyota", 2000);
            Console.WriteLine($"Brand        : {sampleCar.Brand}");
            Console.WriteLine($"Rate Per Day : ₹{sampleCar.RentalRatePerDay}");
            Console.WriteLine($"Days         : 3");
            Console.WriteLine($"Total Rental = ₹{sampleCar.CalculateRental(3)}");

            Console.WriteLine("\n" + new string('─', 40) + "\n");

            // --- Full fleet demo ---
            RentalAgency agency = new RentalAgency();
            agency.AddVehicle(new Car("Toyota Innova",    2000));
            agency.AddVehicle(new Car("Hyundai Creta",    1800));
            agency.AddVehicle(new Bike("Royal Enfield",    800));
            agency.AddVehicle(new Bike("Honda Activa",     400));

            agency.DisplayAllRentals(days: 3);

            // --- Validation tests ---
            Console.WriteLine("\n--- Validation Tests ---");
            new Car("Ford", -500);          // Negative rate blocked
            new Bike("", 300);              // Empty brand blocked

            Vehicle testCar = new Car("Maruti", 1500);
            testCar.CalculateRental(-2);    // Invalid days blocked
            testCar.CalculateRental(0);     // Zero days blocked
        }
    }
}