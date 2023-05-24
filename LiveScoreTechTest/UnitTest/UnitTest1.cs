using LiveScoreLib.Domain;

namespace UnitTest;

public class UnitTest1
{
    [Theory]
    [InlineData("team1", "team2",true, 0,1)]
    [InlineData("team2", "team1",false, 2,3)]
    [InlineData("team4", "team3",true, 1,4)]
    public void GameCreatedOk(string homeTeam, string awayTeam, bool finished, int homeGoals, int awayGoals)
    {
        var game = new Game(homeTeam, awayTeam, finished, homeGoals, awayGoals);
        Assert.Equal(game.HomeTeam, homeTeam);
        Assert.Equal(game.AwayTeam, awayTeam);
        Assert.Equal(game.IsFinish(), finished);
        Assert.Equal(game.HomeScore, homeGoals);
        Assert.Equal(game.AwayScore, awayGoals);
    }

    [Theory]
    [InlineData(TeamType.Away)]
    [InlineData(TeamType.Home)]
    public void GameIncreaseScore(TeamType team)
    {
        var game = new Game("team1", "team2");
        game.IncreaseScore(team);
        Assert.Equal(1, team == TeamType.Away ? game.AwayScore : game.HomeScore);
    }
}