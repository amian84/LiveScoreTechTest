namespace LiveScoreLib.Domain;

public enum TeamType
{
    Home,
    Away
}

internal class Game
{
    public DateTime StartGame { init; get; }
    public string GameId { init; get; }
    public string HomeTeam {init; get; }
    public string AwayTeam {init; get; }
    private bool Finished { set; get; }
    public int HomeScore {private set; get; }
    public int AwayScore {private set; get; }

    public Game(string homeTeam, string awayTeam, bool finished = true, int homeScore = 0, int awayScore = 0)
    {
        HomeTeam = homeTeam;
        HomeScore = homeScore;
        Finished = finished;
        AwayScore = awayScore;
        AwayTeam = awayTeam;
        GameId = Guid.NewGuid().ToString();
        StartGame = DateTime.Now;
    }

    public void UpdateScore(int homeScore, int awayScore)
    {
        HomeScore = homeScore;
        AwayScore = awayScore;
    }

    public void IncreaseScore(TeamType team)
    {
        _ = team == TeamType.Away ? AwayScore++ : HomeScore++;
    }

    public bool IsFinish() => Finished;

    public override string ToString()
    {
        return $"{HomeTeam} {HomeScore} - {AwayScore} {AwayTeam} - Live {Finished}";
    }

    public int TotalScore()
    {
        return HomeScore + AwayScore;
    }

    public void FinishMatch()
    {
        Finished = false;
    }
}