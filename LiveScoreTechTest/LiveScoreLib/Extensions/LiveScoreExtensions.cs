using FluentValidation;
using LiveScoreLib.Application;
using LiveScoreLib.Application.Abstractions;
using LiveScoreLib.Application.Exceptions;
using LiveScoreLib.Application.UseCases;
using LiveScoreLib.Domain;
using LiveScoreLib.Infrastructure;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace LiveScoreLib.Extensions;

public static class LiveScoreExtensions
{
    public static IServiceCollection AddLiveScoreExtensions(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<CreateMatch>();
            })
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddScoped(typeof(IRequestExceptionHandler<,,>), typeof(ExceptionHandler<,,>))
            .AddSingleton<IScore<Game>, LiveScoreGame>()
            .AddSingleton<IRepository<Game>, MemoryRepository>()
            .AddValidatorsFromAssembly(typeof(CreateMatch).Assembly);
    }
}