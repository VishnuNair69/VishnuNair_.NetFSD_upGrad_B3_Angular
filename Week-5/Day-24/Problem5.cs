using System;
using System.IO;

class MacDiskMonitor
{
    static void Main()
    {
        try
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                
                if (!drive.IsReady)
                {
                    Console.WriteLine($"{drive.Name} - Not Ready");
                    continue;
                }

                double totalSizeGB = drive.TotalSize / (1024.0 * 1024 * 1024);
                double freeSpaceGB = drive.AvailableFreeSpace / (1024.0 * 1024 * 1024);
                double freePercent = (freeSpaceGB / totalSizeGB) * 100;

                string status = freePercent < 15 ? "Low Space" : "OK";

                Console.WriteLine($"{drive.Name} - Type: {drive.DriveType}, Total: {totalSizeGB:F2} GB, Free: {freeSpaceGB:F2} GB, Status: {status}");
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine(" No permission to access one or more drives.");
        }
        catch (IOException ex)
        {
            Console.WriteLine(" Drive error: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(" Unexpected error: " + ex.Message);
        }

        Console.WriteLine("\nProgram ended.");
    }
}