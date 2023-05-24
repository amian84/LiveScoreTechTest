using FluentAssertions;
using LiveScoreLib.Application;
using LiveScoreLib.Domain;
using LiveScoreLib.Infrastructure;
using UnitTest.ClassesData;

namespace UnitTest;

public class LiveScoreTest
{
    private LiveScore _liveScore;
    public LiveScoreTest()
    {
        _liveScore = new LiveScore(new MemoryRepository());
    }
    
    [Fact]
    public void LiveScore_Check_Current_Game()
    {
        //Arrange
        var game = new Game("t1", "t2");
        
        //Act
        var notCGame = _liveScore.AreLiveScore();
        _liveScore.SetCurrentGame(game);
        var yesCGame = _liveScore.AreLiveScore();
        
        //Asserts
        Assert.False(notCGame);
        Assert.True(yesCGame);
    }
    
    [Fact]
    public void LiveScore_New_Match_Current_Live()
    {
        //Arrange
        var game1 = new Game("t1", "t2");
        var game2 = new Game("t3", "t4");
        _liveScore.SetCurrentGame(game1);
        
        //Act
        _liveScore.SetCurrentGame(game2);
        var isFirstGame = _liveScore.CheckCurrentGame(game1.GameId);
        //Asserts
        Assert.True(isFirstGame);
    }
    
    [Fact]
    public void LiveScore_Not_Game_Update()
    {
        Assert.False(_liveScore.UpdateScore(2, 1));
    }
    
    [Fact]
    public void LiveScore_Game_Update()
    {
        //Arrange
        var game = new Game("t1", "t2");
        _liveScore.SetCurrentGame(game);
        
        //Act
        _liveScore.UpdateScore(2, 1);
        
        //Assert
        Assert.Equal("t1 2 - 1 t2 - Live True", _liveScore.ShowScore());
        
    }
    
    [Fact]
    public void LiveScore_FinishMatch()
    {
        //Arrange
        var game = new Game("t1", "t2");
        _liveScore.SetCurrentGame(game);
        
        //Act
        _liveScore.FinishMatch();
        var summary = _liveScore.GetSummary();
        
        //Assert
        Assert.False(_liveScore.AreLiveScore());
        Assert.Equal("t1 0 - 0 t2 - Live False" , summary.First());
        
    }
    
    
    [Theory]
    [ClassData(typeof(ListGamesData))]
    public void LiveScore_Summary(ListOfGamesData games)
    {
        //Arrange
        
        foreach (var game in games.Games)
        {
            _liveScore.SetCurrentGame(game);
            if (game.GameId != games.Games.Last().GameId)
                _liveScore.FinishMatch();
        }
        
        //Act
        var summary = _liveScore.GetSummary();
        
        //Assert
        
        Assert.Equal(games.OrderedGames.Count, summary.Count());
        Assert.True(summary.SequenceEqual(games.OrderedGames));
        
        
    }
}