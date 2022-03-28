using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IMBox.Shared.Infrastructure.EmailClient.EmailSmtpClient
{
    public static class Extensions
    {
        public static IServiceCollection AddEmailSmtpClient(this IServiceCollection services)
        {
            services.AddScoped<IEmailClient>(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var smtpSettings = configuration.GetSection(nameof(SmtpSettings)).Get<SmtpSettings>();
                return new SmtpEmailClient(smtpSettings);
            });

            return services;
        }
    }
}