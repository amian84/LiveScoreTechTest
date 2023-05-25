using LiveScoreLib.Application;
using LiveScoreLib.Application.Abstractions;

namespace LiveScoreLib.Domain;

internal class LiveScoreGame : IScore<Game>
{
    private Game? CurrentGame { get; set; }
    private IRepository<Game> Repository { get; set; }

    public LiveScoreGame(IRepository<Game> repository)
    {
        Repository = repository;
    }

    public void SetCurrentGame(Game game)
    {
        if (CurrentGame != null)
            return;
        Repository.TryAddAsync(game);
        CurrentGame?.FinishMatch();
        CurrentGame = game;
        CurrentGame.SetLive();
    }

    public void FinishMatch()
    {
        CurrentGame?.FinishMatch();
        CurrentGame = null;
    }

    public bool AreLiveScore() => CurrentGame != null;
    public string ShowScore()
    {
        return CurrentGame == null ? string.Empty : CurrentGame.ToString();
    }
    
    public bool UpdateScore(int homeScore, int awayScore)
    {
        if (CurrentGame == null)
        {
            return false;
        }
        CurrentGame.UpdateScore(homeScore, awayScore);
        return true;
    }

    public IEnumerable<string> GetSummary()
    {
        var internalSummary = GetSummaryInternal().Result;
        return internalSummary.Order(new GameComparer()).Reverse().Select(g => g.ToString());
    }
    private async Task<IEnumerable<Game>> GetSummaryInternal()
    {
        return await Repository.GetAllAsync();
    }

    public bool CheckCurrentGame(string gameId)
    {
        return CurrentGame?.GameId == gameId;
    }
}