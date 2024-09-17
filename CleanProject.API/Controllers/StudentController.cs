using CleanProject.API.Base;
using CleanProject.Core.Features.Students.Commands.Models;
using CleanProject.Core.Features.Students.Queries.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : AppControllerBase1
    {
        #region Handel Functions
        [HttpPost("Create")]
        public async Task<IActionResult> CreateStudent([FromBody] AddStudentCommand command)
        {
            var student = await _mediator.Send(command);
            return NewResult(student);
        }

        [HttpGet("List")]
        public async Task<IActionResult> GetStudentList()
        {
            var student = await _mediator.Send(new GetStudentListQuery());
            return NewResult(student);
        }

        [HttpGet("Paginated")]
        public async Task<IActionResult> GetStudentPaginated([FromQuery] GetStudentPaginatedQuery query)
        {
            var student = await _mediator.Send(query);
            return Ok(student);
        }


        [HttpGet("GetById")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _mediator.Send(new GetStudentByIdQuery(id));
            return Ok(student);
        }


        [HttpPost("Edit")]
        public async Task<IActionResult> EditStudent([FromBody] EditStudentCommand command)
        {
            var student = await _mediator.Send(command);
            return Ok(student);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> EditStudent(int id)
        {
            var student = await _mediator.Send(new DeleteStudentCommand(id));
            return Ok(student);
        }

        #endregion
    }
}
