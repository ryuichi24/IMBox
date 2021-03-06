using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using IMBox.Shared.Infrastructure.EventBus.MassTransit;
using IMBox.Services.User.Domain.Repositories;
using IMBox.Services.User.Infrastructure.Repositories;
using IMBox.Shared.Infrastructure.Helpers.Hash;
using IMBox.Shared.Infrastructure.Helpers.Auth;
using IMBox.Services.User.Infrastructure.Managers.Auth;
using IMBox.Shared.Infrastructure.EmailClient.EmailSmtpClient;

namespace IMBox.Services.User.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddMemoryCache()
                .AddMongoDB()
                .AddMongoRepositories()
                .AddMassTransitWithRabbitMQ()
                .AddHashHelper()
                .AddJwtAuth()
                .AddManagers()
                .AddEmailSmtpClient();

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

        private static IServiceCollection AddManagers(this IServiceCollection services)
        {
            services.AddSingleton<ITokenManager, TokenManager>();
            return services;
        }
    }
}