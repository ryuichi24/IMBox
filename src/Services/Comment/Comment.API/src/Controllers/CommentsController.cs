using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Services.Comment.API.DTOs;
using IMBox.Services.Comment.Domain.Entities;
using IMBox.Services.Comment.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMBox.Services.Comment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentsController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CommentDTO>))]
        public async Task<IActionResult> GetAsync()
        {
            var comments = await _commentRepository.GetAllAsync();
            return Ok(comments.Select(comment => comment.ToDTO()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentDTO))]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            return Ok(comment.ToDTO());
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CommentDTO))]
        public async Task<IActionResult> CreateAsync(CreateCommentDTO createCommentDTO)
        {
            var newComment = new CommentEntity
            {
                Text = createCommentDTO.Text,
                MovieId = createCommentDTO.MovieId,
                UserId = createCommentDTO.UserId
            };

            await _commentRepository.CreateAsync(newComment);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = newComment.Id }, newComment);
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