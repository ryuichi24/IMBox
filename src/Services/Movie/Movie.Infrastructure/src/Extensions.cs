using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using IMBox.Shared.Infrastructure.EventBus.MassTransit;
using IMBox.Services.Movie.Domain.Repositories;
using IMBox.Services.Movie.Infrastructure.Repositories;

namespace IMBox.Services.Movie.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMongoDB()
                    .AddMongoRepositories()
                    .AddMassTransitWithRabbitMQ();

            return services;
        }

        private static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoMovieRepository(database, "movies");
            });

            services.AddScoped<IMemberRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoMemberRepository(database, "members");
            });

            return services;
        }
    }
}