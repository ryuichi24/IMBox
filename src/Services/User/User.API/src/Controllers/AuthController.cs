using System;
using System.Threading.Tasks;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.User.API.DTOs;
using IMBox.Services.User.Domain.Entities;
using IMBox.Services.User.Domain.Repositories;
using IMBox.Services.User.Infrastructure.Managers.Auth;
using IMBox.Shared.Infrastructure.Helpers.Hash;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.User.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHashHelper _hashHelper;
        private readonly IPublishEndpoint _publishEndpoint;

        public AuthController(IUserRepository UserRepository, ITokenManager tokenManager, IHashHelper hashHelper, IPublishEndpoint publishEndpoint)
        {
            _UserRepository = UserRepository;
            _tokenManager = tokenManager;
            _hashHelper = hashHelper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        public async Task<IActionResult> SignupAsync(SignupDTO signupDTO)
        {
            var existingUser = await _UserRepository.GetByEmailAsync(signupDTO.Email);

            // might be better to return 200 OK because client should not know which email is registered.
            if (existingUser != null) return BadRequest("The email is already registered.");

            var (passwordHash, passwordHashSalt) = _hashHelper.CreateHash(signupDTO.Password);

            var newUser = new UserEntity
            {
                Username = signupDTO.Username,
                Email = signupDTO.Email,
                PasswordHash = passwordHash,
                PasswordHashSalt = passwordHashSalt,
            };

            // TODO: send confirmation email

            await _UserRepository.CreateAsync(newUser);

            return Ok();
        }

        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponseDTO))]
        public async Task<IActionResult> SigninAsync(SigninDTO signinDTO)
        {
            var existingUser = await _UserRepository.GetByEmailAsync(signinDTO.Email);

            if (existingUser == null) return BadRequest("The email or password is invalid.");

            if (!existingUser.IsActive) return BadRequest("The email or password is invalid.");

            var isValidPassword = _hashHelper.VerifyHash(signinDTO.Password, existingUser.PasswordHash, existingUser.PasswordHashSalt);

            if (!isValidPassword) return BadRequest("The email or password is invalid.");

            return Ok(new TokenResponseDTO
            {
                AccessToken = _tokenManager.createAccessToken(existingUser),
                RefreshToken = _tokenManager.createRefreshToken(existingUser.Id)
            });
        }

        [HttpGet("confirm-email/{token}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        public async Task<IActionResult> ConfirmEmailAsync(string token)
        {
            var existingUser = await _UserRepository.GetByEmailConfirmTokenAsync(token);

            if (existingUser == null) return BadRequest("The email confirm token is invalid.");

            if (existingUser.IsActive) return Ok();

            existingUser.RevokeEmailConfirmToken();
            existingUser.Activate();

            await _UserRepository.UpdateAsync(existingUser);

            await _publishEndpoint.Publish(new UserCreatedIntegrationEvent
            {
                UserId = existingUser.Id,
                UserUsername = existingUser.Username,
                UserBirthDate = existingUser.BirthDate,
                UserGender = existingUser.Gender,
                UserCountry = existingUser.Country
            });

            return Ok();
        }

        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TokenResponseDTO))]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenDTO refreshTokenDTO)
        {
            var userId = _tokenManager.VerifyRefreshToken(refreshTokenDTO.RefreshToken);
            _tokenManager.RevokeRefreshToken(refreshTokenDTO.RefreshToken);

            if (userId == default(Guid))
            {
                return Unauthorized("The refresh token is invalid.");
            }

            var existingUser = await _UserRepository.GetByIdAsync(userId);

            if (existingUser == null)
            {
                return Unauthorized("The refresh token is invalid.");
            }

            return Ok(new TokenResponseDTO
            {
                AccessToken = _tokenManager.createAccessToken(existingUser),
                RefreshToken = _tokenManager.createRefreshToken(existingUser.Id)
            });
        }
    }
}