using LiveScoreLib.Application.UseCases;
using LiveScoreLib.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest;

public class MediatorTest
{
    private ServiceCollection _serviceCollection;
    private readonly IMediator _mediator;
    public MediatorTest()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.AddLiveScoreExtensions();
        var serviceProvider = _serviceCollection.BuildServiceProvider();
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
        Assert.True(result.IsFaulted);
        result.IfFail(err => Assert.Equal("There are a currently match", err.Message));
    }
}