using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Departments.Commands.Models
{
    public class DeleteDepartmentCommand : IRequest<IResult<bool>>
    {
        public int Id { get; set; }
        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }
    }
}
