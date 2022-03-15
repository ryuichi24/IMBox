using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Services.Member.API.DTOs;
using IMBox.Services.IntegrationEvents;
using IMBox.Services.Member.Domain.Entities;
using IMBox.Services.Member.Domain.Repositories;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Member.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public MembersController(IMemberRepository memberRepository, IMovieRepository movieRepository, IPublishEndpoint publishEndpoint)
        {
            _memberRepository = memberRepository;
            _movieRepository = movieRepository;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MemberDTO>))]
        public async Task<IActionResult> GetAsync()
        {
            var members = await _memberRepository.GetAllAsync();
            var memberDTOs = await Task.WhenAll(members.Select(async (member) =>
            {
                var movies = await _movieRepository.GetAllByMemberIdAsync(member.Id);
                return member.ToDTO(movies);
            }));

            return Ok(memberDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MemberDTO))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) return NotFound();

            var movies = await _movieRepository.GetAllByMemberIdAsync(member.Id);
            return Ok(member.ToDTO(movies));
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MemberDTO))]
        public async Task<IActionResult> CreateAsync(CreateMemberDTO createMemberDTO)
        {
            var member = new MemberEntity
            {
                Name = createMemberDTO.Name,
                Description = createMemberDTO.Description,
                BirthDate = createMemberDTO.BirthDate,
                Role = createMemberDTO.Role
            };

            await _memberRepository.CreateAsync(member);

            await _publishEndpoint.Publish(new MemberCreatedIntegrationEvent
            {
                MemberName = member.Name,
                MemberId = member.Id,
                MemberRole = member.Role
            });

            // https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            return CreatedAtAction(nameof(GetByIdAsync), new { id = member.Id }, member.ToDTO());
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateMemberDTO updateMemberDTO)
        {
            var existingMember = await _memberRepository.GetByIdAsync(id);

            if (existingMember == null) return BadRequest("No member found");

            existingMember
                .UpdateName(updateMemberDTO.Name)
                .UpdateDescription(updateMemberDTO.Description)
                .UpdateBirthDate(updateMemberDTO.BirthDate)
                .UpdateRole(updateMemberDTO.Role);

            await _memberRepository.UpdateAsync(existingMember);

            await _publishEndpoint.Publish(new MemberUpdatedIntegrationEvent
            {
                MemberName = existingMember.Name,
                MemberId = existingMember.Id,
                MemberRole = existingMember.Role
            });

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var memberToDelete = await _memberRepository.GetByIdAsync(id);

            if (memberToDelete == null) return BadRequest("No member found");

            await _memberRepository.RemoveAsync(memberToDelete.Id);

            await _publishEndpoint.Publish(new MemberDeletedIntegrationEvent
            {
                MemberId = memberToDelete.Id,
            });

            return NoContent();
        }
    }
}