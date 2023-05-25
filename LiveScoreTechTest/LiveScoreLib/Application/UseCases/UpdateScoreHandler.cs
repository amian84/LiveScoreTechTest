using LiveScoreLib.Application.Abstractions;
using LiveScoreLib.Domain;
using MediatR;

namespace LiveScoreLib.Application.UseCases;

public record UpdateScore(int HomeTeamScore, int AwayTeamScore) : IRequest<CustomResult>;


internal class UpdateScoreHandler: IRequestHandler<UpdateScore, CustomResult>
{
    
    private readonly IScore<Game> _liveScoreGame;

    public UpdateScoreHandler(IScore<Game> liveScore)
    {
        _liveScoreGame = liveScore;
    }

    public Task<CustomResult> Handle(UpdateScore request, CancellationToken cancellationToken)
    {
        return Task.FromResult(!_liveScoreGame.AreLiveScore() ? CustomResult.Success(false) :
            CustomResult.Success(_liveScoreGame.UpdateScore(request.HomeTeamScore, request.AwayTeamScore)));
    }
}