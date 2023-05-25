using LiveScoreLib.Application.Abstractions;
using LiveScoreLib.Domain;
using LiveScoreLib.Infrastructure;

namespace UnitTest;

public class RepositoryTest
{
    private IRepository<Game> _repository;
    private Game _game;
    public RepositoryTest()
    {
        _repository = new MemoryRepository();
        _game = new Game("t1", "t2", false);
    }
    [Fact]
    public void Add_Element()
    {
        _repository.TryAddAsync(_game);
        Assert.Contains(_game, _repository.GetAllAsync().Result);
    }
    
    [Fact]
    public void Remove_Element()
    {
        _repository.TryAddAsync(_game);
        Assert.Contains(_game, _repository.GetAllAsync().Result);
        _repository.TryRemoveAsync(_game);
        Assert.Empty(_repository.GetAllAsync().Result);
    }
    
    [Fact]
    public void Exists_Element()
    {
        _repository.TryAddAsync(_game);
        Assert.True(_repository.Exists(_game).Result);
        
    }
}