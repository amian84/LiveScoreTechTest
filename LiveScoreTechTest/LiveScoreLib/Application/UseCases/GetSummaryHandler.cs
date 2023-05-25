using LiveScoreLib.Application.Abstractions;
using LiveScoreLib.Domain;
using MediatR;

namespace LiveScoreLib.Application.UseCases;

public record GetSummary() : IRequest<CustomResult>;


internal class GetSummaryHandler: IRequestHandler<GetSummary, CustomResult>
{
    private readonly IScore<Game> _liveScoreGame;

    public GetSummaryHandler(IScore<Game> liveScore)
    {
        _liveScoreGame = liveScore;
    }
    public Task<CustomResult> Handle(GetSummary request, CancellationToken cancellationToken)
    {
        return Task.FromResult(CustomResult.Success(_liveScoreGame.GetSummary()));
    }
}