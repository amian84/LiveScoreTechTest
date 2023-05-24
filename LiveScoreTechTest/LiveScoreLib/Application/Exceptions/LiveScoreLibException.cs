namespace LiveScoreLib.Application.Exceptions;

public class LiveScoreLibException:Exception
{
    public LiveScoreLibException(string message)
        : base(message)
    {
    }
}