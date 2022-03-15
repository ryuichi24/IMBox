using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using IMBox.Shared.Infrastructure.EventBus.MassTransit;
using IMBox.Services.User.Domain.Repositories;
using IMBox.Services.User.Infrastructure.Repositories;

namespace IMBox.Services.User.Infrastructure
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
            services.AddScoped<IUserRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoUserRepository(database, "Users");
            });

            return services;
        }
    }
}