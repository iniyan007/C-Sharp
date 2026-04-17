using System.Collections.Generic;
using System.Linq;

public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
{
    private readonly List<T> _items = new List<T>();

    public void Add(T entity) => _items.Add(entity);

    public T Get(int id) => _items.FirstOrDefault(x => x.Id == id);

    public IEnumerable<T> GetAll() => _items;

    public void Update(T entity)
    {
        var existing = Get(entity.Id);
        if (existing != null)
        {
            _items.Remove(existing);
            _items.Add(entity);
        }
    }

    public void Delete(int id)
    {
        var entity = Get(id);
        if (entity != null)
            _items.Remove(entity);
    }
}