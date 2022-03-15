using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Services.Member.API.DTOs;
using IMBox.Services.User.API.DTOs;
using IMBox.Services.User.Domain.Entities;
using IMBox.Services.User.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _UserRepository;
        public UsersController(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
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

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserDTO))]
        public async Task<IActionResult> CreateAsync(CreateUserDTO createUserDTO)
        {
            var newUser = new UserEntity
            {
                Email = createUserDTO.Email,
                BirthDate = createUserDTO.BirthDate,
                Continent = createUserDTO.Continent,
                Gender = createUserDTO.Gender,
            };

            await _UserRepository.CreateAsync(newUser);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newUser.Id }, newUser);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateUserDTO updateUserDTO)
        {
            var userToUpdate = await _UserRepository.GetByIdAsync(id);

            if (userToUpdate == null) return BadRequest("No user found");

            await _UserRepository.UpdateAsync(userToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var userToDelete = await _UserRepository.GetByIdAsync(id);

            if (userToDelete == null) return BadRequest("No User found");

            await _UserRepository.RemoveAsync(userToDelete.Id);

            return NoContent();
        }
    }
}