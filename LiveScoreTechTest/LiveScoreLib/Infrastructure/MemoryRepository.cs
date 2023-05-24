using System.Collections.Concurrent;
using LiveScoreLib.Application;
using LiveScoreLib.Domain;

namespace LiveScoreLib.Infrastructure;

public class MemoryRepository:IRepository<Game>
{
    private ConcurrentDictionary<int, Game> InMemoryDb { get; } = new();
    public Task<IEnumerable<Game>> GetAllAsync()
    {
        return Task.FromResult(InMemoryDb.Values.Select(x=>x));
    }

    public Task<bool> TryAddAsync(Game entity)
    {
        var last = InMemoryDb.Count;
        return Task.FromResult(InMemoryDb.TryAdd(last + 1, entity));
    }

    public Task<bool> Exists(Game entity)
    {
        return Task.FromResult(InMemoryDb.Values.Contains(entity));
    }

    public Task<bool> TryRemoveAsync(Game entity)
    {
        var pair = InMemoryDb.First(kv => kv.Value == entity);
        return Task.FromResult(InMemoryDb.TryRemove(pair));
    }

}