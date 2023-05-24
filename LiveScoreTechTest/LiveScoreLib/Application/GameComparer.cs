using LiveScoreLib.Domain;

namespace LiveScoreLib.Application;

internal class GameComparer:IComparer<Game>
{
    public int Compare(Game? x, Game? y)
    {
        if (x == null && y != null)
        {
            return -1;
        }
        if (x != null && y == null)
        {
            return 1;
        }
        if (x?.TotalScore() > y?.TotalScore())
        {
            return 1;
        }
        if (x?.TotalScore() == y?.TotalScore())
        {
            return x?.StartGame<=y?.StartGame ? 1 : -1;
        }

        return -1;
    }
}