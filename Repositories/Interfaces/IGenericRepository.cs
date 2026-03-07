namespace DeliveryApi.Repositories.Interfaces;

//where T : class significa que T solo puede ser una clase, no un int o bool por ejemplo.
public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task SaveAsync();
}