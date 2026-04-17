using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting async operations...\n");

        try
        {
            Task<string> task1 = FetchDataAsync("Source 1", 2000);
            Task<string> task2 = FetchDataAsync("Source 2", 3000);
            Task<string> task3 = FetchDataAsync("Source 3", 1500);
            string[] results = await Task.WhenAll(task1, task2, task3);
            Console.WriteLine("\nAll tasks completed. Aggregated Results:\n");
            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }

    static async Task<string> FetchDataAsync(string sourceName, int delay)
    {
        try
        {
            Console.WriteLine($"{sourceName} fetching started...");
            await Task.Delay(delay);
            if (new Random().Next(0, 5) == 0)
            {
                throw new Exception($"{sourceName} failed!");
            }
            Console.WriteLine($"{sourceName} completed.");
            return $"{sourceName} data retrieved successfully.";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in {sourceName}: {ex.Message}");
            throw;
        }
    }
}