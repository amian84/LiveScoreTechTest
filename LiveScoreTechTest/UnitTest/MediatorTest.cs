using LiveScoreLib.Application.UseCases;
using LiveScoreLib.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UnitTest.ClassesData;

namespace UnitTest;

public class MediatorTest
{
    private readonly IMediator _mediator;
    public MediatorTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddLiveScoreExtensions();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task Create_Match()
    {
        var result = await _mediator.Send(new CreateMatch("Team1", "Team2"));
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task Create_Match_Without_Finish()
    {
        var _ = await _mediator.Send(new CreateMatch("Team1", "Team2"));
        var result = await _mediator.Send(new CreateMatch("Team3", "Team4"));
        Assert.True(result.IsFailed);
        Assert.Equal("There are a currently match", result.GetExceptionMessage());
    }
    
    [Fact]
    public async Task Create_Match_Empty_Team()
    {
        var result = await _mediator.Send(new CreateMatch("", "Team2"));
        
        Assert.True(result.IsFailed);   
        Assert.Contains("Validation failed", result.GetExceptionMessage());
    }
    
    [Fact]
    public async Task Finish_Match_Without_Live()
    {
        var result = await _mediator.Send(new FinishGame());
        Assert.True(result.IsSuccess);
        Assert.False(result.GetValue<bool>());
    }
    
    [Fact]
    public async Task Finish_Match_With_Live()
    {
        var _ = await _mediator.Send(new CreateMatch("Team1", "Team2"));

        var result = await _mediator.Send(new FinishGame());
        Assert.True(result.IsSuccess);
        Assert.True(result.GetValue<bool>());
    }
    
    [Fact]
    public async Task Update_Score_Without_Live()
    {
        var result = await _mediator.Send(new UpdateScore(1, 3));

        Assert.True(result.IsSuccess);
        Assert.False(result.GetValue<bool>());
    }
    
    [Fact]
    public async Task Update_Score_With_Live()
    {
        var _ = await _mediator.Send(new CreateMatch("Team1", "Team2"));

        var result = await _mediator.Send(new UpdateScore(1, 3));

        Assert.True(result.IsSuccess);
        Assert.True(result.GetValue<bool>());
    }
    
    [Fact]
    public async Task Update_Score_Negative_Score()
    {
        var _ = await _mediator.Send(new CreateMatch("Team1", "Team2"));

        var result = await _mediator.Send(new UpdateScore(-1, 3));

        Assert.True(result.IsFailed);   
        Assert.Contains("Validation failed", result.GetExceptionMessage());
    }
    
    [Theory]
    [ClassData(typeof(ListGamesData))]
    public async Task Get_Summary(ListOfGamesData games)
    {
        //Arrange
        
        foreach (var game in games.Games)
        {
            var _ = await _mediator.Send(new CreateMatch(game.HomeTeam,game.AwayTeam));
            _ = await _mediator.Send(new UpdateScore(game.HomeScore,game.AwayScore));
            if (game.GameId != games.Games.Last().GameId)
                _ = await _mediator.Send(new FinishGame());
        }
        
        //Act
        var result = await _mediator.Send(new GetSummary());
        
        //Assert
        Assert.True(result.IsSuccess);   
        Assert.Equal(games.OrderedGames.Count, result.GetValue<IEnumerable<string>>().ToList().Count);
        Assert.True(result.GetValue<IEnumerable<string>>().ToList().SequenceEqual(games.OrderedGames));
        
    }
}