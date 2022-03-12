using IMBox.Shared.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace IMBox.Shared.Database.MongoDB
{
    public static class Extensions
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDBSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();

                var mongoClient = new MongoClient(mongoDBSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });
            return services;
        }
    }
}