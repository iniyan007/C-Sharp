using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<string> items = new List<string>();
        bool running = true;
        while (running)
        {
            
           
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Remove Item");
            Console.WriteLine("3. Display Items");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddItem(items);
                    break;

                case "2":
                    RemoveItem(items);
                    break;

                case "3":
                    DisplayItems(items);
                    break;

                case "4":
                    running = false;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid choice! Try again.");
                    break;
            }
        }
    }

    static void AddItem(List<string> items)
    {
        Console.Write("Enter item to add: ");
        string input = Console.ReadLine().Trim();

        if (!string.IsNullOrEmpty(input))
        {
            input = input.ToUpper(); 
            items.Add(input);
            Console.WriteLine("Item added successfully!");
        }
        else
        {
            Console.WriteLine("Invalid input.");
        }
    }

    static void RemoveItem(List<string> items)
    {
        if (items.Count == 0)
        {
            Console.WriteLine("List is empty.");
            return;
        }

        Console.Write("Enter item to remove: ");
        string input = Console.ReadLine().Trim().ToUpper();

        if (items.Remove(input))
        {
            Console.WriteLine("Item removed successfully!");
        }
        else
        {
            Console.WriteLine("Item not found.");
        }
    }

    static void DisplayItems(List<string> items)
    {
        if (items.Count == 0)
        {
            Console.WriteLine("No items to display.");
            return;
        }

        Console.WriteLine("\n--- Items List ---");
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }
        Console.WriteLine("--- End of List ---\n");
    }
}