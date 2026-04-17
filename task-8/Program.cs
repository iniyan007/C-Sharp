class Program
{
    static void Main(string[] args)
    {
        IRepository<Product> repo = new InMemoryRepository<Product>();
        ProductService service = new ProductService(repo);
        ConsoleUI ui = new ConsoleUI(service);

        ui.Run();
    }
}