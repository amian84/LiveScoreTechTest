namespace LiveScoreLib.Application;

internal interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<bool> TryAddAsync(T entity);
    Task<bool> Exists(T entity);
    Task<bool> TryRemoveAsync(T entity);
}