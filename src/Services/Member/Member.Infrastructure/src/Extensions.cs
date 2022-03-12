using IMBox.Services.Member.Domain.Repositories;
using IMBox.Services.Member.Infrastructure.Repositories;
using IMBox.Shared.Infrastructure.Database.MongoDB;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace IMBox.Services.Member.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddMongoDB()
                    .AddMongoRepositories();

            return services;
        }

        private static IServiceCollection AddMongoRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMemberRepository>(serviceProvider =>
            {
                var database = serviceProvider.GetService<IMongoDatabase>();
                return new MongoMemberRepository(database, "members");
            });

            return services;
        }
    }
}