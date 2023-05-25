using LiveScoreLib.Application.Abstractions;
using LiveScoreLib.Domain;
using MediatR;

namespace LiveScoreLib.Application.UseCases;


public record FinishGame() : IRequest<CustomResult>;

internal class FinishGameHandler: IRequestHandler<FinishGame, CustomResult>
{
    
    private readonly IScore<Game> _liveScoreGame;

    public FinishGameHandler(IScore<Game> liveScore)
    {
        _liveScoreGame = liveScore;
    }
    
    public Task<CustomResult> Handle(FinishGame request, CancellationToken cancellationToken)
    {
        if (!_liveScoreGame.AreLiveScore())
        {
            return Task.FromResult(CustomResult.Success(false));
        }
        _liveScoreGame.FinishMatch();
        return Task.FromResult(CustomResult.Success(true));
    }
}