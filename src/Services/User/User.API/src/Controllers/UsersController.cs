using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Member.API.DTOs;
using IMBox.Services.User.API.DTOs;
using IMBox.Services.User.Domain.Entities;
using IMBox.Services.User.Domain.Repositories;
using IMBox.Shared.Infrastructure.Helpers.Hash;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.User.API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        private readonly IHashHelper _hashHelper;
        private readonly IPublishEndpoint _publishEndpoint;

        public UsersController(IUserRepository UserRepository, IHashHelper hashHelper, IPublishEndpoint publishEndpoint)
        {
            _UserRepository = UserRepository;
            _hashHelper = hashHelper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDTO>))]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _UserRepository.GetAllAsync();
            return Ok(users.Select(User => User.ToDTO()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDTO))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _UserRepository.GetByIdAsync(id);
            return Ok(user.ToDTO());
        }

        // AllowAnonymous is for development purpose
        [AllowAnonymous]
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        public async Task<IActionResult> CreateAsync(CreateUserDTO createUserDTO)
        {
            var (passwordHash, passwordHashSalt) = _hashHelper.CreateHash(createUserDTO.Password);

            var newUser = new UserEntity
            {
                Username = createUserDTO.Username,
                Email = createUserDTO.Email,
                PasswordHash = passwordHash,
                PasswordHashSalt = passwordHashSalt,
                BirthDate = createUserDTO.BirthDate,
                Country = createUserDTO.Country,
                Gender = createUserDTO.Gender,
                IsActive = true,
                Roles = createUserDTO.Roles
            };

            await _UserRepository.CreateAsync(newUser);

            await _publishEndpoint.Publish(new UserCreatedIntegrationEvent
            {
                UserId = newUser.Id,
                UserUsername = newUser.Username,
                UserBirthDate = newUser.BirthDate,
                UserGender = newUser.Gender,
                UserCountry = newUser.Country
            });

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newUser.Id }, newUser.ToDTO());
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateUserDTO updateUserDTO)
        {
            var userToUpdate = await _UserRepository.GetByIdAsync(id);

            if (userToUpdate == null) return BadRequest("No user found");

            userToUpdate
                .UpdateUsername(updateUserDTO.Username)
                .UpdateEmail(updateUserDTO.Email)
                .UpdateGender(updateUserDTO.Gender)
                .UpdateBirthDate(updateUserDTO.BirthDate)
                .UpdateCountry(updateUserDTO.Country)
                .UpdateRoles(updateUserDTO.Roles);

            if (!string.IsNullOrWhiteSpace(updateUserDTO.Password))
            {
                var (passwordHash, passwordHashSalt) = _hashHelper.CreateHash(updateUserDTO.Password);
                userToUpdate
                    .UpdatePasswordHash(passwordHash)
                    .UpdatePasswordHashSalt(passwordHashSalt);
            }

            await _UserRepository.UpdateAsync(userToUpdate);

            await _publishEndpoint.Publish(new UserUpdatedIntegrationEvent
            {
                UserId = userToUpdate.Id,
                UserUsername = userToUpdate.Username,
                UserBirthDate = userToUpdate.BirthDate,
                UserGender = userToUpdate.Gender,
                UserCountry = userToUpdate.Country
            });

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userToDelete = await _UserRepository.GetByIdAsync(id);

            if (userToDelete == null) return BadRequest("No User found");

            await _UserRepository.RemoveAsync(userToDelete.Id);

            await _publishEndpoint.Publish(new UserDeletedIntegrationEvent
            {
                UserId = userToDelete.Id
            });

            return NoContent();
        }
    }
}