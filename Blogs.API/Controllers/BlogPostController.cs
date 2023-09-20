using Blogs.Application.DTO.BlogPost;
using Blogs.Application.Exceptions;
using Blogs.Application.Services.Abstractions;
using Blogs.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Blogs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostService _blogPostService;

        public BlogPostController(IBlogPostService blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _blogPostService.GetAllAsync());
            } catch(Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlog(Guid id)
        {
            try
            {
                return Ok(await _blogPostService.GetAsync(id));
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(CreateBlogPostDTO blogPost)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Ok(await _blogPostService.CreateAsync(blogPost, userId));
            } catch(BusinessException ex)
            {
                return BadRequest(ex.Message);
            } catch(Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBlog(UpdateBlogPostDTO blogPost)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                return Ok(await _blogPostService.UpdateAsync(blogPost, userId));
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(Guid id)
        {
            try
            {
                var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _blogPostService.DeleteAsync(id, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
