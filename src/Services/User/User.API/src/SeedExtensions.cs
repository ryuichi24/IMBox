using System.Collections.Generic;
using IMBox.Services.User.API.Settings;
using IMBox.Services.User.Domain.Entities;
using IMBox.Services.User.Domain.Repositories;
using IMBox.Shared.Infrastructure.Helpers.Hash;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IMBox.Services.IntegrationEvents;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace IMBox.Services.User.API
{
    public static class Extensions
    {
        public static async Task AddAdminUser(this WebApplication app)
        {
            var serviceProvider = app.Services;
            var configuration = serviceProvider.GetService<IConfiguration>();
            var adminSettings = configuration.GetSection(nameof(AdminSettings)).Get<AdminSettings>();

            var publishEndpoint = serviceProvider.GetService<IPublishEndpoint>();
            var hashHelper = serviceProvider.GetService<IHashHelper>();
            var userRepository = serviceProvider.GetService<IUserRepository>();

            var existingAdminUser = await userRepository.GetByEmailAsync(adminSettings.AdminEmail);

            if (existingAdminUser != null) return;

            var (passwordHash, passwordHashSalt) = hashHelper.CreateHash(adminSettings.AdminPassword);

            var newAdminUser = new UserEntity
            {
                Username = adminSettings.AdminUsername,
                Email = adminSettings.AdminEmail,
                PasswordHash = passwordHash,
                PasswordHashSalt = passwordHashSalt,
                IsActive = true,
                Roles = new List<string> {
                    "admin"
                }
            };

            await userRepository.CreateAsync(newAdminUser);

            await publishEndpoint.Publish(new UserCreatedIntegrationEvent
            {
                UserId = newAdminUser.Id,
                UserUsername = newAdminUser.Username,
                UserBirthDate = newAdminUser.BirthDate,
                UserGender = newAdminUser.Gender,
                UserCountry = newAdminUser.Country
            });

        }
    }
}