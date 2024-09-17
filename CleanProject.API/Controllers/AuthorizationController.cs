using CleanProject.API.Base;
using CleanProject.Core.Features.Authorization.Commands.Models;
using CleanProject.Core.Features.Authorization.Queries.Modles;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : AppControllerBase
    {
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromForm] AddRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpPost("EditRole")]
        public async Task<IActionResult> EditRole([FromForm] EditRoleCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var response = await _mediator.Send(new DeleteRoleCommand { Id = Id });
            return NewResult(response);
        }


        [HttpGet("RoleList")]
        public async Task<IActionResult> GetRoleList()
        {
            var response = await _mediator.Send(new GetRoleListQuery());
            return NewResult(response);
        }

        [HttpGet("GetRoleById")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var response = await _mediator.Send(new GetRoleByIdQuery { Id = id });
            return NewResult(response);
        }

        //[SwaggerOperation(Summary = " ادارة صلاحيات المستخدمين", OperationId = "ManageUserRoles")]
        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles(int userId)
        {
            var response = await _mediator.Send(new ManageUserRolesQuery { UserId = userId });
            return NewResult(response);
        }

        [HttpPost("UpdateUserRoles")]
        public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesCommand command)
        {
            var response = await _mediator.Send(command);
            return NewResult(response);
        }


    }
}
