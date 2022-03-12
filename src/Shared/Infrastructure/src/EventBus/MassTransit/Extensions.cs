using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MassTransit;
using MassTransit.Definition;
using IMBox.Shared.Core.Settings;

namespace IMBox.Shared.Infrastructure.EventBus.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMassTransitWithRabbitMQ(this IServiceCollection services)
        {
            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumers(Assembly.GetEntryAssembly());

                busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
                {
                    var configuration = context.GetService<IConfiguration>();
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                    busFactoryConfigurator.Host(rabbitMQSettings.Host);
                    busFactoryConfigurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, includeNamespace: false));
                });
            });

            services.AddMassTransitHostedService();

            return services;
        }
    }
}