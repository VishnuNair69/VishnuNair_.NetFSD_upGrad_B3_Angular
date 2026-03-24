using System;
using System.Diagnostics;
using System.IO;

namespace OrderProcessingTracing
{
    class Program
    {
        static void Main(string[] args)
        {
            string logFilePath = "OrderProcessingLog.txt";
            Trace.Listeners.Clear(); 
            Trace.Listeners.Add(new TextWriterTraceListener(logFilePath));
            Trace.AutoFlush = true; 

            try
            {
                Console.WriteLine("Order Processing Started...");

                ValidateOrder();
                ProcessPayment();
                UpdateInventory();
                GenerateInvoice();

                Console.WriteLine("Order Processing Completed!");
                Trace.TraceInformation("Order processed successfully.\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Order Processing Failed!");
                Trace.TraceError($"Error: {ex.Message}");
            }
        }

        static void ValidateOrder()
        {
            Trace.WriteLine("Step 1: Validating order...");
            bool isValid = true;
            if (!isValid)
            {
                throw new Exception("Order validation failed.");
            }
            Trace.TraceInformation("Order validation passed.");
        }

        static void ProcessPayment()
        {
            Trace.WriteLine("Step 2: Processing payment...");
            bool paymentSuccess = true;
            if (!paymentSuccess)
            {
                throw new Exception("Payment processing failed.");
            }
            Trace.TraceInformation("Payment processed successfully.");
        }

        static void UpdateInventory()
        {
            Trace.WriteLine("Step 3: Updating inventory...");
            bool inventoryUpdated = true;
            if (!inventoryUpdated)
            {
                throw new Exception("Inventory update failed.");
            }
            Trace.TraceInformation("Inventory updated successfully.");
        }

        static void GenerateInvoice()
        {
            Trace.WriteLine("Step 4: Generating invoice...");
            bool invoiceGenerated = true;
            if (!invoiceGenerated)
            {
                throw new Exception("Invoice generation failed.");
            }
            Trace.TraceInformation("Invoice generated successfully.");
        }
    }
}