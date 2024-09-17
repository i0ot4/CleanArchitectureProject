using CleanProject.API.Base;
using CleanProject.Core.Features.Authentication.Commands.Models;
using CleanProject.Core.Features.Authentication.Queries.Modles;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {

        [HttpPost("SginIn")]
        public async Task<IActionResult> SginIn([FromQuery] SignInCommand signInCommand)
        {
            return NewResult(await _mediator.Send(signInCommand));
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet("ValidateToken")]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            var response = await _mediator.Send(query);
            return NewResult(response);
        }
    }
}

