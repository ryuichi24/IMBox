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
        private readonly IPublishEndpoint _publishEndpoint;

        public MembersController(IMemberRepository memberRepository, IPublishEndpoint publishEndpoint)
        {
            _memberRepository = memberRepository;
            _publishEndpoint = publishEndpoint;
        }

        // GET /members
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MemberDTO>))]
        public async Task<IActionResult> GetAsync()
        {
            var members = await _memberRepository.GetAllAsync();
            return Ok(members.Select(member => member.ToDTO()));
        }

        // GET /members/{id}
        [HttpGet("{id}")]
        [ProducesResponseType((int)200, Type = typeof(MemberDTO))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member.ToDTO());
        }

        // POST /members
        [HttpPost()]
        [ProducesResponseType((int)201, Type = typeof(MemberDTO))]
        public async Task<IActionResult> Create(CreateMemberDTO createMemberDTO)
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
            });

            // https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            return CreatedAtAction(nameof(GetById), new { id = member.Id }, member.ToDTO());
        }

        // PUT /members/{id}
        [HttpPut("{id}")]
        [ProducesResponseType((int)204, Type = typeof(void))]
        public async Task<IActionResult> Update(Guid id, UpdateMemberDTO updateMemberDTO)
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
            });

            return NoContent();
        }

        // DELETE /members/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType((int)204, Type = typeof(void))]
        public async Task<IActionResult> Delete(Guid id)
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