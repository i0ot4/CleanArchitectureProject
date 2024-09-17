using CleanProject.API.Base;
using CleanProject.Core.Features.Departments.Commands.Models;
using CleanProject.Core.Features.Departments.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : AppControllerBase
    {

        #region Handel Functions

        [HttpPost("Department/Create")]
        public async Task<IActionResult> CreateDepartment([FromBody] AddDepartmentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDepartmentList()
        {
            return NewResult(await _mediator.Send(new GetDepartmentListQuery()));
        }

        [HttpGet("ById")]
        public async Task<IActionResult> GetDepartmentById([FromQuery] GetDepartmentByIdQuery query)
        {
            return NewResult(await _mediator.Send(query));
        }

        [HttpGet("Department/Paginated")]
        public async Task<IActionResult> GetDepartmentPaginated([FromQuery] GetDepartmentPaginatedListQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }


        [HttpPost("Department/Edit")]
        public async Task<IActionResult> EditDepartmentById([FromBody] EditDepartmentCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("Department/Delete")]
        public async Task<IActionResult> DeleteDepartmentById([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteDepartmentCommand(id));
            return Ok(response);
        }

        #endregion
    }
}
