using System.Collections;
using LiveScoreLib.Domain;

namespace UnitTest.ClassesData;

public class ListOfGamesData
{
    internal List<Game> Games { get; set; } 
    internal List<string> OrderedGames { get; set; }
}

public class ListGamesData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new ListOfGamesData()
            {
                Games = new List<Game>()
                {
                    new ("t1","t2", false, 1, 1),
                    new ("t3","t4", false, 3, 2)    
                },
                OrderedGames = new List<string>()
                {
                    "t3 3 - 2 t4 - Live True",
                    "t1 1 - 1 t2 - Live False"
                } 
                
            }
            
        };
        yield return new object[]
        {
            new ListOfGamesData()
            {
                Games = new List<Game>()
                {
                    new ("t3","t4", false, 1, 1),
                    new ("t1","t2", false, 1, 1),
                } ,
                OrderedGames = new List<string>()
                {
                    "t3 1 - 1 t4 - Live False",
                    "t1 1 - 1 t2 - Live True"
                }   
            }
        };
        yield return new object[]
        {
            new ListOfGamesData()
            {
                Games = new List<Game>()
                {
                    new ("t1","t2", false, 3, 2),        
                    new ("t3","t4", false, 1, 8)
                },
                OrderedGames = new List<string>()
                {
                    "t3 1 - 8 t4 - Live True",
                    "t1 3 - 2 t2 - Live False"
                }   
            }
            
        };
        
        yield return new object[]
        {
            new ListOfGamesData()
            {
                Games = new List<Game>()
                {
                    new ("t1","t2", false, 3, 2),        
                    new ("t3","t4", false, 2, 1),
                    new ("t5","t6", false, 0, 4),        
                    new ("t7","t8", false, 1,4),
                    new ("t11","t12", false, 1, 2),
                    new ("t9","t10", false, 8, 5),        
                },
                OrderedGames = new List<string>()
                {
                    "t9 8 - 5 t10 - Live True",
                    "t1 3 - 2 t2 - Live False",
                    "t7 1 - 4 t8 - Live False",
                    "t5 0 - 4 t6 - Live False",
                    "t3 2 - 1 t4 - Live False",
                    "t11 1 - 2 t12 - Live False",
                }   
            }
            
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}