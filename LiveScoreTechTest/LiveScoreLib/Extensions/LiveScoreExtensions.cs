using LiveScoreLib.Application.Abstractions;
using LiveScoreLib.Application.UseCases;
using LiveScoreLib.Domain;
using LiveScoreLib.Infrastructure;
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
            .AddSingleton<IScore<Game>, LiveScoreGame>()
            .AddSingleton<IRepository<Game>, MemoryRepository>();
    }
}