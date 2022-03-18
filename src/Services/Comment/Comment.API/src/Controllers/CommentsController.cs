using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMBox.Core.StringHelpers;
using IMBox.Services.Comment.API.DTOs;
using IMBox.Services.Comment.Domain.Entities;
using IMBox.Services.Comment.Domain.Repositories;
using IMBox.Services.IntegrationEvents;
using IMBox.Shared.Infrastructure.Helpers.Auth;
using MassTransit;
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
        private readonly IPublishEndpoint _publishEndpoint;

        public CommentsController(ICommentRepository commentRepository, ICommenterRepository commenterRepository, IPublishEndpoint publishEndpoint)
        {
            _commentRepository = commentRepository;
            _commenterRepository = commenterRepository;
            _publishEndpoint = publishEndpoint;
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
            var newComment = new CommentEntity
            {
                Text = createCommentDTO.Text,
                MovieId = createCommentDTO.MovieId,
                CommenterId = User.SubjectId().ToGuid()
            };

            await _commentRepository.CreateAsync(newComment);

            await _publishEndpoint.Publish(new CommentCreatedIntegrationEvent
            {
                CommentId = newComment.Id,
                MovieId = newComment.MovieId,
                CommenterId = newComment.CommenterId,
                Text = newComment.Text
            });

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

            await _publishEndpoint.Publish(new CommentDeletedIntegrationEvent
            {
                CommentId = commentToDelete.Id,
                MovieId = commentToDelete.MovieId
            });

            return NoContent();
        }
    }
}