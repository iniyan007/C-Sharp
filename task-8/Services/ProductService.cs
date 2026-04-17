using System.Collections.Generic;

public class ProductService
{
    private readonly IRepository<Product> _repository;

    public ProductService(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public void AddProduct(Product product)
    {
        if (product.Price <= 0)
            throw new System.Exception("Price must be greater than zero");

        _repository.Add(product);
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _repository.GetAll();
    }

    public void UpdateProduct(Product product)
    {
        _repository.Update(product);
    }

    public void DeleteProduct(int id)
    {
        _repository.Delete(id);
    }
}