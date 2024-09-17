using CleanProject.Core.Bases;
using CleanProject.Core.Features.Departments.Queries.Results;
using MediatR;

namespace CleanProject.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentByIdQuery : IRequest<Response<GetDepartmentByIdResponse>>
    {
        public int Id { get; set; }
        public int StudentPageNumber { get; set; }
        public int StudentPageSize { get; set; }
    }
}
