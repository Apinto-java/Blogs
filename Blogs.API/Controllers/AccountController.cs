using Blogs.Application.DTO.User;
using Blogs.Application.Exceptions;
using Blogs.Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blogs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                var response = await _userService.LoginAsync(loginRequest);
                return Ok(response);
            } catch (BusinessException ex) 
            {
                return BadRequest(ex.Message);
            } catch(Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequest)
        {
            try
            {
                await _userService.RegisterAsync(registerRequest);
                return Ok();
            } catch(BusinessException ex)
            {
                return BadRequest(ex.Message);
            } catch(Exception ex)
            {
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }
    }
}
