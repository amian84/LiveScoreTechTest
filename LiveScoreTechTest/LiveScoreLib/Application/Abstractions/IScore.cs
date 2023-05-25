namespace LiveScoreLib.Application.Abstractions;

public interface IScore<T>
{
    public void SetCurrentGame(T game);
    public void FinishMatch();
    public bool AreLiveScore();
    public bool UpdateScore(int homeScore, int awayScore);
    public IEnumerable<string> GetSummary();
}