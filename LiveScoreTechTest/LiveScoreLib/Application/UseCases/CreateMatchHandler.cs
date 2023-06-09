using LiveScoreLib.Application.Abstractions;
using LiveScoreLib.Application.Exceptions;
using LiveScoreLib.Domain;
using MediatR;

namespace LiveScoreLib.Application.UseCases;

public record CreateMatch(string HomeTeam, string AwayTeam) : IRequest<CustomResult>;

internal class CreateMatchHandler : IRequestHandler<CreateMatch, CustomResult>
{
    
    private readonly IScore<Game> _liveScoreGame;

    public CreateMatchHandler(IScore<Game> liveScore)
    {
        _liveScoreGame = liveScore;
    }
    public Task<CustomResult> Handle(CreateMatch request, CancellationToken cancellationToken)
    {
        if (_liveScoreGame.AreLiveScore())
        {
            var error = new LiveScoreLibException("There are a currently match");
            return Task.FromResult(CustomResult.Fail(error));
        }
        var game = new Game(request.HomeTeam, request.AwayTeam);
        _liveScoreGame.SetCurrentGame(game);
        return Task.FromResult(CustomResult.Success(game.GameId));
    }
}