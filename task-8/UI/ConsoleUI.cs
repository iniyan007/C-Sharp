using System;

public class ConsoleUI
{
    private readonly ProductService _service;

    public ConsoleUI(ProductService service)
    {
        _service = service;
    }

    public void Run()
    {
        _service.AddProduct(new Product { Id = 1, Name = "Laptop", Price = 80000 });
        _service.AddProduct(new Product { Id = 2, Name = "Phone", Price = 30000 });

        Console.WriteLine("Products:");
        foreach (var p in _service.GetAllProducts())
        {
            Console.WriteLine($"{p.Id} - {p.Name} - ₹{p.Price}");
        }
    }
}