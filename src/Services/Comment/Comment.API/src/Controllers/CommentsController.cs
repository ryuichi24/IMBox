using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Services.Comment.API.DTOs;
using IMBox.Services.Comment.Domain.Entities;
using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Shared.Infrastructure.Helpers.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Comment.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ICommenterRepository _commenterRepository;

        public CommentsController(ICommentRepository commentRepository, ICommenterRepository commenterRepository)
        {
            _commentRepository = commentRepository;
            _commenterRepository = commenterRepository;
        }

        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CommentDTO>))]
        public async Task<IActionResult> GetAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            var commenters = await _commenterRepository.GetAllAsync();

            var commentDTOs = comments.Select(comment => comment.ToDTO(commenters.Find(commenter => commenter.Id == comment.CommenterId)));

            return Ok(commentDTOs);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDTO))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            var commenter = await _commenterRepository.GetByIdAsync(comment.CommenterId);
            return Ok(comment.ToDTO(commenter));
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CommentDTO))]
        public async Task<IActionResult> CreateAsync(CreateCommentDTO createCommentDTO)
        {
            Guid.TryParse(User.SubjectId(), out Guid userId);

            var newComment = new CommentEntity
            {
                Text = createCommentDTO.Text,
                MovieId = createCommentDTO.MovieId,
                CommenterId = userId
            };

            await _commentRepository.CreateAsync(newComment);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newComment.Id }, newComment.ToDTO());
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCommentDTO updateCommentDTO)
        {
            var commentToUpdate = await _commentRepository.GetByIdAsync(id);

            if (commentToUpdate == null) return BadRequest("No comment found");

            commentToUpdate
                .UpdateText(updateCommentDTO.Text);

            await _commentRepository.UpdateAsync(commentToUpdate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(void))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var commentToDelete = await _commentRepository.GetByIdAsync(id);

            if (commentToDelete == null) return BadRequest("No comment found");

            await _commentRepository.RemoveAsync(commentToDelete.Id);

            return NoContent();
        }
    }
}