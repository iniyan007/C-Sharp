using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Starting Runnable App...\n");

        RunnerService runner = new RunnerService();
        runner.ExecuteAll();

        Console.WriteLine("\nDone.");
    }
}