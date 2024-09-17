using CleanProject.API.Base;
using CleanProject.Core.Features.Users.Commands.Models;
using CleanProject.Core.Features.Users.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AppControllerBase
    {



        [HttpPost("/Create")]
        public async Task<IActionResult> CreateUser([FromBody] AddUserCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet("/List")]
        public async Task<IActionResult> GetUsers([FromQuery] GetUserListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("/ById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery { Id = id });
            return Ok(response);
        }

        [HttpPost("/Edit")]
        public async Task<IActionResult> EditUser([FromBody] EditUserCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }

        [HttpPost("/Delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return NewResult(await _mediator.Send(new DeleteUserCommand { Id = id }));
        }

        [HttpPost("/ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            return NewResult(await _mediator.Send(command));
        }


    }
}
