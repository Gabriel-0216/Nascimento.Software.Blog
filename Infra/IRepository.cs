namespace Infra
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> Remove(T entity);
        string GetConnection();
    }
}
