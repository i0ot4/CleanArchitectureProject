using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Departments.Commands.Models
{
    public class EditDepartmentCommand : IRequest<IResult<string>>
    {
        public int Id { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
    }
}
