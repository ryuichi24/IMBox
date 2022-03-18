using System.Threading.Tasks;
using IMBox.Services.User.API.DTOs;
using IMBox.Services.User.Domain.Entities;
using IMBox.Services.User.Domain.Repositories;
using IMBox.Services.User.Infrastructure.Managers.Auth;
using IMBox.Shared.Infrastructure.Helpers.Hash;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        private readonly ITokenManager _tokenManager;
        private readonly IHashHelper _hashHelper;

        public AuthController(IUserRepository UserRepository, ITokenManager tokenManager, IHashHelper hashHelper)
        {
            _UserRepository = UserRepository;
            _tokenManager = tokenManager;
            _hashHelper = hashHelper;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(void))]
        public async Task<IActionResult> SigninAsync(SigninDTO signinDTO)
        {
            var existingUser = await _UserRepository.GetByEmailAsync(signinDTO.Email);

            if (existingUser == null) return BadRequest("The email or password is invalid.");

            if (!existingUser.IsActive) return BadRequest("The email or password is invalid.");

            var isValidPassword = _hashHelper.VerifyHash(signinDTO.Password, existingUser.PasswordHash, existingUser.PasswordHashSalt);

            if (!isValidPassword) return BadRequest("The email or password is invalid.");

            return Ok(new
            {
                AccessToken = _tokenManager.createAccessToken(existingUser)
            });
        }
    }
}