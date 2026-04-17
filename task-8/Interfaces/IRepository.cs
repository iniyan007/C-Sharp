using System.Collections.Generic;

public interface IRepository<T> where T : class, IEntity
{
    void Add(T entity);
    T Get(int id);
    IEnumerable<T> GetAll();
    void Update(T entity);
    void Delete(int id);
}