using Microsoft.Extensions.DependencyInjection;

namespace IMBox.Shared.Infrastructure.Helpers.Hash
{
    public static class Extensions
    {
        public static IServiceCollection AddHashHelper(this IServiceCollection services)
        {
            services.AddSingleton<IHashHelper, HashHelper>();
            return services;
        }
    }
}