using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Services.Rating.Infrastructure.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using IMBox.Shared.Infrastructure.EventBus.MassTransit;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IMBox.Services.Rating.Infrastructure
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
            services.AddScoped<IRatingRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRatingRepository(database, "ratings");
            });

            services.AddScoped<IMovieRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoMovieRepository(database, "movies");
            });

            return services;
        }
    }
}