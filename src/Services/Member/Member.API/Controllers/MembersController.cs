using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IMBox.Services.Member.API.DTOs;
using IMBox.Services.Member.Domain.Entities;
using IMBox.Services.Member.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Member.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;

        public MembersController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        // GET /members
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MemberDTO>))]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _memberRepository.GetAllAsync());
        }

        // GET /members/{id}
        [HttpGet("{id}")]
        [ProducesResponseType((int)200, Type = typeof(MemberDTO))]
        public async Task<IActionResult> GetById(Guid id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member);
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

            // https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
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

            return NoContent();
        }

        // DELETE /members/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType((int)204, Type = typeof(void))]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _memberRepository.RemoveAsync(id);

            return NoContent();
        }
    }
}