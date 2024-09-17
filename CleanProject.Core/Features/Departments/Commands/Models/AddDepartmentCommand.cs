using CleanProject.Core.Bases;
using MediatR;

namespace CleanProject.Core.Features.Departments.Commands.Models
{
    public class AddDepartmentCommand : IRequest<IResult<string>>
    {
        public string DNameAr { get; set; }
        public string DNameEn { get; set; }
    }
}
