namespace LiveScoreLib.Domain;

public class Game
{
    public string HomeTeam {init; get; }
    public string AwayTeam {init; get; }
    private bool Finished { get; set; }
    public int HomeScore {private set; get; }
    public int AwayScore {private set; get; }

    public Game(string homeTeam, string awayTeam, bool finished, int homeScore=0, int awayScore=0)
    {
        HomeTeam = homeTeam;
        HomeScore = homeScore;
        Finished = finished;
        AwayScore = awayScore;
        AwayTeam = awayTeam;
    }

    public bool IsFinish() => Finished;
}