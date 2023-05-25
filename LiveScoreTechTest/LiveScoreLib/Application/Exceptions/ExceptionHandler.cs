using MediatR;
using MediatR.Pipeline;

namespace LiveScoreLib.Application.Exceptions;

public class ExceptionHandler<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TResponse : class
    where TRequest : IRequest<TResponse>
    where TException : Exception
{
    
    public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
    {

        var handledResult = CustomResult.Fail(exception);
        state.SetHandled(handledResult as TResponse ?? throw new Exception("Handlers must return Result"));
        return Task.CompletedTask;
    }
}

