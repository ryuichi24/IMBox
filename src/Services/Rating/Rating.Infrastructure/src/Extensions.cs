using IMBox.Services.Rating.Domain.Entities;
using IMBox.Services.Rating.Domain.Repositories;
using IMBox.Services.Rating.Infrastructure.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using IMBox.Shared.Infrastructure.EventBus.MassTransit;
using IMBox.Shared.Infrastructure.Helpers.Auth;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace IMBox.Services.Rating.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            RegisterMongoClassMap();

            services.AddMongoDB()
                    .AddMongoRepositories()
                    .AddMassTransitWithRabbitMQ()
                    .AddJwtAuth();

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

            services.AddScoped<IRaterRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoRaterRepository(database, "raters");
            });

            return services;
        }

        private static void RegisterMongoClassMap()
        {
            BsonClassMap.RegisterClassMap<RatingEntity>(classMap => 
            {
                classMap.AutoMap();
                classMap.UnmapMember(member => member.Rater);
            });
        }
    }
}