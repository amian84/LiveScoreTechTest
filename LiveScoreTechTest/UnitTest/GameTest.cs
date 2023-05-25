using LiveScoreLib.Application;
using LiveScoreLib.Domain;
namespace UnitTest;

public class GameTest
{
    [Theory]
    [InlineData("team1", "team2",true, 0,1)]
    [InlineData("team2", "team1",false, 2,3)]
    [InlineData("team4", "team3",true, 1,4)]
    public void Game_Created_Ok(string homeTeam, string awayTeam, bool finished, int homeGoals, int awayGoals)
    {
        //Arrange
        
        //Act
        var game = new Game(homeTeam, awayTeam, finished, homeGoals, awayGoals);
        
        //Assert
        Assert.Equal(homeTeam, game.HomeTeam);
        Assert.Equal(awayTeam, game.AwayTeam);
        Assert.Equal(finished, game.IsFinish());
        Assert.Equal(homeGoals, game.HomeScore);
        Assert.Equal(awayGoals, game.AwayScore);
        Assert.True(DateTime.Now>game.StartGame);
    }

    [Theory]
    [InlineData(TeamType.Away)]
    [InlineData(TeamType.Home)]
    public void Game_Increase_Score(TeamType team)
    {
        //Arrange
        var game = new Game("team1", "team2");
        //Act
        game.IncreaseScore(team);
        //Assert
        Assert.Equal(1, team == TeamType.Away ? game.AwayScore : game.HomeScore);
    }
    
    [Theory]
    [InlineData(1,3)]
    [InlineData(2,5)]
    public void Game_Update_Score(int homeScore, int awayScore)
    {
        //Arrange
        var game = new Game("team1", "team2");
        //Act
        game.UpdateScore(homeScore, awayScore);
        //Assert
        Assert.Equal(homeScore, game.HomeScore);
        Assert.Equal(awayScore, game.AwayScore);
    }
    
    [Theory]
    [InlineData(1,3, 4)]
    [InlineData(2,5, 7)]
    public void Game_Total_Score(int homeScore, int awayScore, int expected)
    {
        //Arrange
        var game = new Game("team1", "team2", true, homeScore, awayScore);
        //Act
        var total = game.TotalScore();
        //Assert
        Assert.Equal(expected, total);
    }
    
    [Theory]
    [InlineData(1,3, "team1 1 - 3 team2 - Live True")]
    [InlineData(2,5, "team1 2 - 5 team2 - Live True")]
    public void Game_ToString(int homeScore, int awayScore, string expected)
    {
        //Arrange
        var game = new Game("team1", "team2", false, homeScore, awayScore);
        //Act
        var gameStr = game.ToString();
        //Assert
        Assert.Equal(expected, gameStr);
    }
    
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Game_Null_Game(bool g1null)
    {
        //Arrange
        var game = new Game("team1", "team2");
        //Act
        var list = new List<Game?>();
        list.Add(g1null ? null : game);
        list.Add(g1null ? game : null);

        list.Sort(new GameComparer());
        //Assert
        Assert.Equal(game, list[1]);
    }
    
    
}
