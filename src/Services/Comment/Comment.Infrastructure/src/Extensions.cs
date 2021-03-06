using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Services.Comment.Infrastructure.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using IMBox.Shared.Infrastructure.EventBus.MassTransit;
using IMBox.Shared.Infrastructure.Helpers.Auth;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IMBox.Services.Comment.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMongoDB()
                    .AddMongoRepositories()
                    .AddMassTransitWithRabbitMQ()
                    .AddJwtAuth();

            return services;
        }

        private static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICommentRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoCommentRepository(database, "comments");
            });

            services.AddScoped<ICommenterRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoCommenterRepository(database, "commenters");
            });

            return services;
        }
    }
}