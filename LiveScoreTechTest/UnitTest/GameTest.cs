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
        Assert.Equal(game.HomeTeam, homeTeam);
        Assert.Equal(game.AwayTeam, awayTeam);
        Assert.Equal(game.IsFinish(), finished);
        Assert.Equal(game.HomeScore, homeGoals);
        Assert.Equal(game.AwayScore, awayGoals);
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
}