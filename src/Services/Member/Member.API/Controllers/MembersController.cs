using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Services.Member.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Member.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private static readonly List<MemberDTO> _members = new()
        {
            new MemberDTO(Id: Guid.NewGuid(), Name: "Tom Holland", Description: "He is Spiderman.", BirthDate: DateTime.Now.Date, Role: "cast"),
            new MemberDTO(Id: Guid.NewGuid(), Name: "Robert Downey Jr.", Description: "He is Ironman.", BirthDate: DateTime.Now.Date, Role: "cast"),
            new MemberDTO(Id: Guid.NewGuid(), Name: "Chris Evans", Description: "He is Captain America.", BirthDate: DateTime.Now.Date, Role: "cast"),
        };

        // GET /members
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MemberDTO>))]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(_members);
        }

        // GET /members/{id}
        [HttpGet("{id}")]
        [ProducesResponseType((int)200, Type = typeof(MemberDTO))]
        public IActionResult GetById(Guid id)
        {
            var member = _members.Where(member => member.Id == id).SingleOrDefault();
            if (member == null) return NotFound();
            return Ok(member);
        }

        // POST /members
        [HttpPost()]
        [ProducesResponseType((int)201, Type = typeof(MemberDTO))]
        public IActionResult Create(CreateMemberDTO createMemberDTO)
        {
            var member = new MemberDTO
            (
                Id: Guid.NewGuid(),
                Name: createMemberDTO.Name,
                Description: createMemberDTO.Description,
                BirthDate: createMemberDTO.BirthDate,
                Role: createMemberDTO.Role
            );

            _members.Add(member);

            // https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            return CreatedAtAction(nameof(GetById), new { id = member.Id }, member);
        }

        // PUT /members/{id}
        [HttpPut("{id}")]
        [ProducesResponseType((int)204, Type = typeof(void))]
        public IActionResult Update(Guid id, UpdateMemberDTO updateMemberDTO)
        {
            var existingMember = _members.Where(member => member.Id == id).SingleOrDefault();

            if (existingMember == null) return BadRequest("No member found");

            var updatedMember = existingMember with
            {
                Name = updateMemberDTO.Name,
                Description = updateMemberDTO.Description,
                BirthDate = updateMemberDTO.BirthDate,
                Role = updateMemberDTO.Role
            };

            var index = _members.FindIndex(member => member.Id == id);
            _members[index] = updatedMember;

            return NoContent();
        }

        // DELETE /members/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType((int)204, Type = typeof(void))]
        public IActionResult Delete(Guid id)
        {
            var index = _members.FindIndex(member => member.Id == id);
            if (index < 0) return BadRequest("No member found");

            _members.RemoveAt(index);

            return NoContent();
        }
    }
}