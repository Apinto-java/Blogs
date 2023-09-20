using Blogs.Application.DTO.Comment;
using Blogs.Application.Exceptions;
using Blogs.Application.Services.Abstractions;
using Blogs.Application.Services.Implementations;
using Blogs.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blogs.API.Controllers
{
    [Route("api/BlogPost/{blogId}/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid blogId)
        {
            try
            {
                return Ok(await _commentService.GetBlogPostComments(blogId));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(Guid blogId, Guid id)
        {
            try
            {
                return Ok(await _commentService.GetCommentOfBlogPost(blogId, id));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPost("/api/[controller]")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDTO comment)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Ok(await _commentService.CreateAsync(comment, userId));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPut("/api/[controller]")]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentDTO comment)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Ok(await _commentService.UpdateAsync(comment, userId));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpDelete("/api/[controller]/{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _commentService.DeleteAsync(id, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
