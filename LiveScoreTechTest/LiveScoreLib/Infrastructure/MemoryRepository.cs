using System.Collections.Concurrent;
using LiveScoreLib.Application;
using LiveScoreLib.Domain;

namespace LiveScoreLib.Infrastructure;

internal class MemoryRepository:IRepository<Game>
{
    private ConcurrentDictionary<string, Game> InMemoryDb { get; } = new();
    public Task<IEnumerable<Game>> GetAllAsync()
    {
        return Task.FromResult(InMemoryDb.Values.Select(x=>x));
    }

    public Task<bool> TryAddAsync(Game entity)
    {
        return Task.FromResult(InMemoryDb.TryAdd(entity.GameId, entity));
    }

    public Task<bool> Exists(Game entity)
    {
        return Task.FromResult(InMemoryDb.ContainsKey(entity.GameId));
    }

    public Task<bool> TryRemoveAsync(Game entity)
    {
        var pair = InMemoryDb.First(kv => kv.Key == entity.GameId);
        return Task.FromResult(InMemoryDb.TryRemove(pair));
    }

}